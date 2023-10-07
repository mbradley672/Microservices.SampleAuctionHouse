using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Entities;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;

[ApiController, Route("api/search")]
public class SearchController: ControllerBase {
    public SearchController() {
        
    }

    [HttpGet()]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery]SearchParams searchParams) {
        var query = DB.PagedSearch<Item, Item>();

        if (searchParams.SearchTerm is not null or "")
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch {
            "make" => query.Sort(x => x.Ascending(c => c.Make)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
        };

        query = searchParams.FilterBy switch {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.Add(new TimeSpan(5, 0, 0, 0)) && x.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(x=>x.AuctionEnd > DateTime.UtcNow)
        };

        if (searchParams.Seller is not null or "")
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        if (searchParams.Winner is not null or "")
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();

        return Ok(new {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}