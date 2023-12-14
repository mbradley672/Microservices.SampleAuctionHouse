using BiddingService.Models;
using Contracts.Contracts;
using MassTransit;
using MongoDB.Entities;

namespace BiddingService.Services;

public class CheckAuctionFinished: BackgroundService {
    private readonly ILogger<CheckAuctionFinished> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CheckAuctionFinished(ILogger<CheckAuctionFinished> logger, IServiceProvider serviceProvider) {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        _logger.LogInformation("Starting check for finished auctions");
        stoppingToken.Register(() => _logger.LogInformation("==> AuctionCheck is stopping"));
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckAuctions(stoppingToken);
            await Task.Delay(5000, stoppingToken);
        }
    }

    private async Task CheckAuctions(CancellationToken stoppingToken) {
        var finishedAuctions = await DB.Find<Auction>()
                                       .Match(x => x.AuctionEnd < DateTime.UtcNow && x.Finished == false)
                                       .ExecuteAsync(stoppingToken);
        
        if (!finishedAuctions.Any()) return;
        
        _logger.LogInformation($"Found {finishedAuctions.Count} finished auctions");
        
        using var scope = _serviceProvider.CreateScope();
        var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        foreach (var auction in finishedAuctions)
        {
            auction.Finished = true;
            await auction.SaveAsync(cancellation: stoppingToken);
            
            var winningBid = await DB.Find<Bid>()
                                     .Match(x => x.AuctionId == auction.ID)
                                     .Sort(x=>x.Descending(x => x.Amount))
                                     .ExecuteFirstAsync(stoppingToken);

            await endpoint.Publish(new AuctionFinished() {
                ItemSold = winningBid != null,
                AuctionId = auction.ID,
                Winner = winningBid?.Bidder,
                Seller = auction.Seller,
                Amount = winningBid.Amount,
            }, stoppingToken);

        }
    }
}