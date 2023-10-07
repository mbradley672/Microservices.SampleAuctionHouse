using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace AuctionService.Dtos;

public class CreateAuctionDto
{
    [Required] public string Make { get; set; } 
    [Required] public string Model { get; set; }
    [Required] public string Color { get; set; }
    [Required] public int Year { get; set; }
    [Required] public int Mileage { get; set; }
    [Required] public int ReservePrice { get; set; }
    [Required] public string ImageUrl { get; set; }
    [Required] public DateTime AuctionEnd { get; set; }
}