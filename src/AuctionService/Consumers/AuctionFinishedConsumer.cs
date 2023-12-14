using AuctionService.Data;
using AuctionService.Entities;
using Contracts.Contracts;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration.Audit;

namespace AuctionService.Consumers;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished> {
    private readonly AuctionDbContext _dbContext;

    public AuctionFinishedConsumer(AuctionDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<AuctionFinished> consumerContext) {
        Console.WriteLine($"--> Consuming AuctionFinished Message: {consumerContext.Message.AuctionId}");
        var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(consumerContext.Message.AuctionId));
        if (auction != null && consumerContext.Message.ItemSold)
        {
            auction.Winner = consumerContext.Message.Winner;
            auction.SoldAmount = consumerContext.Message.Amount;

            auction.Status = auction.SoldAmount > auction.ReservePrice 
                ? Status.Finished : Status.ReserveNotMet;
        }

        await _dbContext.SaveChangesAsync();
    }
}