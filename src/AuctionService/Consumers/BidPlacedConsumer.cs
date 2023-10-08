using System.Diagnostics;
using AuctionService.Data;
using Contracts.Contracts;
using MassTransit;

namespace AuctionService.Consumers;

public class BidPlacedConsumer : IConsumer<BidPlaced> {
    private readonly AuctionDbContext _dbContext;

    public BidPlacedConsumer(AuctionDbContext dbContext) {
        _dbContext = dbContext;
    }
    public async Task Consume(ConsumeContext<BidPlaced> consumerContext) {
        Console.WriteLine($"--> Consuming BidPlaced: {consumerContext.Message.AuctionId}");
        var auction = await _dbContext.Auctions.FindAsync(consumerContext.Message.AuctionId);

        Debug.Assert(auction != null, nameof(auction) + " != null");
        if (auction.CurrentHighBid == null || consumerContext.Message.BidStatus.Contains("Accepted") && consumerContext.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = consumerContext.Message.Amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}