using Contracts.Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers; 

public class BidPlacedConsumer: IConsumer<BidPlaced> {
    public BidPlacedConsumer() {
        
    }

    public async Task Consume(ConsumeContext<BidPlaced> consumerContext) {
        Console.WriteLine("--> Consuming BidPlaced: " + consumerContext.Message.Id);

        var auction = await DB.Find<Item>().OneAsync(consumerContext.Message.Id);

        if (auction.CurrentHighBid == null || consumerContext.Message.BidStatus.Contains("Accepted") &&
            consumerContext.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = consumerContext.Message.Amount;
            await auction.SaveAsync();
        }
    }
}