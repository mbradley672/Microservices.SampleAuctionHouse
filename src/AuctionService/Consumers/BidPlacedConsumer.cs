using System.Diagnostics;
using AuctionService.Data;
using Contracts.Contracts;
using MassTransit;

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'

namespace AuctionService.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced> {
    private readonly AuctionDbContext _dbContext;

    public BidPlacedConsumer(AuctionDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<BidPlaced> consumerContext) {
        Console.WriteLine($"--> Consuming BidPlaced: {consumerContext.Message.AuctionId}");
        var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(consumerContext.Message.AuctionId));

        Debug.Assert(auction != null, nameof(auction) + " != null");
        if (auction.CurrentHighBid == null || consumerContext.Message.BidStatus.Contains("Accepted") &&
            consumerContext.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = consumerContext.Message.Amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}