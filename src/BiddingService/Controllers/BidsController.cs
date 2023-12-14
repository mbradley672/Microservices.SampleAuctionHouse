using System.Security.Claims;
using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace BiddingService.Controllers;

[ApiController, Route("api/[controller]")]
public class BidsController(IMapper mapper, IPublishEndpoint publishEndpoint) : ControllerBase {
    [Authorize, HttpPost]
    public async Task<ActionResult<BidDto>> PlaceBid(string auctionId, int amount) {
        var auction = await DB.Find<Auction>().OneAsync(auctionId);
        if (auction == null) return NotFound(); // TODO: Check with auction service if it has the auction

        if (auction.Seller == User.Identity.Name)
        {
            return BadRequest("You can't bid on your own auction");
        }

        var bid = new Bid() {
            Amount = amount,
            AuctionId = auctionId,
            Bidder = User.Identity.Name
            
        };
        
        if (auction.AuctionEnd < DateTime.UtcNow) {
            bid.BidStatus = BidStatus.Finished;
        }
        else
        {
            var highestBid = await DB.Find<Bid>()
                                     .Match(b => b.AuctionId == auctionId)
                                     .Sort(b => b.Descending(x=>x.Amount))
                                     .ExecuteFirstAsync();

            if (highestBid != null && highestBid.Amount >= amount || highestBid == null)
            {
                bid.BidStatus = amount >= auction.ReservePrice ? BidStatus.Accepted : BidStatus.AcceptedBelowReserve;
            }
        
            if (highestBid != null && highestBid.Amount <= amount)
            {
                bid.BidStatus = BidStatus.TooLow;
            }
        }

        await DB.SaveAsync(bid);

        await publishEndpoint.Publish(mapper.Map<BidPlaced>(bid));
        
        return Ok(mapper.Map<BidDto>(bid));
    }

    [HttpGet("{auctionId}")]
    public async Task<ActionResult<IEnumerable<BidDto>>> GetBidsForAuction(string auctionId) {
        var auction = await DB.Find<Auction>().OneAsync(auctionId);
        if (auction == null) return NotFound(); // TODO: Check with auction service if it has the auction

        var bids = await DB.Find<Bid>()
                           .Match(b => b.AuctionId == auctionId)
                           .Sort(b => b.Descending(x => x.BidTime))
                           .ExecuteAsync();

        return bids.Select(mapper.Map<BidDto>).ToList();
    }

}