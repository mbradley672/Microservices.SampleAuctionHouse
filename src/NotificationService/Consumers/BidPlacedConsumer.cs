﻿using Contracts.Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

public class BidPlacedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<BidPlaced> {
    public async Task Consume(ConsumeContext<BidPlaced> context) {
        await hubContext.Clients.All.SendAsync("AuctionFinished", context.Message);
    }
}