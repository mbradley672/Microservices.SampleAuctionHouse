using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace AuctionService.Dtos;

public class AuctionDto
{
    public Guid Id { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }
    [Required] public string Seller { get; set; } 
    [Required] public string Winner { get; set; } 
    [Required] public string Make { get; set; }  
    [Required] public string Model { get; set; } 
    [Required] public string Color { get; set; } 
    [Required] public int Year { get; set; }
    [Required] public int Mileage { get; set; }
    [Required] public string Status { get; set; } 
    [Required] public int ReservePrice { get; set; }
    [Required] public string ImageUrl { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
}