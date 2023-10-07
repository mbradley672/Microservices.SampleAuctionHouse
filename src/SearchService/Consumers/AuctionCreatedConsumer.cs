﻿using AutoMapper;
using Contracts.Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Entities;

namespace SearchService.Consumers {
    public class AuctionCreatedConsumer : IConsumer<AuctionCreatedContract> {
        private readonly IMapper _mapper;

        public AuctionCreatedConsumer(IMapper mapper) {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionCreatedContract> context) {
            Console.WriteLine("--> consuming AuctionCreatedContract: " + context.Message.Id);

            var item = _mapper.Map<Item>(context.Message);

            await item.SaveAsync();
        }
    }
}
