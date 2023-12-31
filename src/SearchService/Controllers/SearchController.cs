﻿using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Entities;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;

[ApiController, Route("api/search")]
public class SearchController : ControllerBase {
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams) {
        var query = DB.PagedSearch<Item, Item>();
        if (searchParams.SearchTerm is not null or "")
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }
        //var test = await query.ExecuteAsync();

        query = searchParams.OrderBy switch {
            "make" => query.Sort(x => x.Ascending(c => c.Make)).Sort(x=>x.Ascending(a=>a.Model)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
        };

        //var sortTesting = await query.ExecuteAsync();

        query = searchParams.FilterBy switch {
            "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x =>
                x.AuctionEnd < DateTime.UtcNow.Add(new TimeSpan(5, 0, 0, 0)) && x.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow) // Change for production must be >
        };

        //var dateTesting = await query.ExecuteAsync();

        if (searchParams.Seller is not null or "")
        {
            query.Match(x => x.Seller == searchParams.Seller);
        }

        if (searchParams.Winner is not null or "")
        {
            query.Match(x => x.Winner == searchParams.Winner);
        }

        query.PageNumber(searchParams.PageNumber ?? 1);
        query.PageSize(searchParams.PageSize ?? 12);

        var result = await query.ExecuteAsync();

        return Ok(new {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}