﻿using MongoDB.Entities;

namespace SearchService.Entities;

public class Item : Entity
{ 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }

    public string Make { get; set; } = string.Empty;
    public string Seller { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public string Model { get; set; } =string.Empty;
    public string Color { get; set; } =string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public int Year { get; set; }
    public int Mileage { get; set; }
    public int ReservePrice { get; set; }
    public int SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
}