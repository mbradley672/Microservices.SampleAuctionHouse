﻿using Contracts.Contracts;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using NotificationService.Hubs;

namespace NotificationService.Consumers;

public class AuctionFinishedConsumer(IHubContext<NotificationHub> hubContext) : IConsumer<AuctionFinished> {
    public async Task Consume(ConsumeContext<AuctionFinished> context) {
        await hubContext.Clients.All.SendAsync("AuctionFinished", context.Message);
    }
}