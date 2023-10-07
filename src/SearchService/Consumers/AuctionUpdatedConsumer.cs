using AutoMapper;
using Contracts.Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated> {
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper) {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AuctionUpdated> context) {
        Console.WriteLine("--> Consuming AuctionUpdated: " + context.Message.Id);

        var newItem = _mapper.Map<Item>(context.Message);
        newItem.UpdatedAt = DateTime.UtcNow;

        var result = await DB.Update<Item>()
                             .Match(x => x.ID == context.Message.Id)
                             .ModifyOnly(x => new {
                                 x.Make, x.Model, x.Color, x.Year, x.Mileage
                             }, newItem)
                             .Modify(x => x.Set(a => a.UpdatedAt, DateTime.UtcNow))
                             .ExecuteAsync();

        if (!result.IsAcknowledged)
            throw new MessageException(typeof(AuctionUpdated), "Problem updating the record in MongoDB");
    }
}