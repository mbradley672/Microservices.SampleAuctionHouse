using System.IO.Enumeration;
using AuctionService.Data;
using AuctionService.Dtos;
using AuctionService.Entities;
using AutoMapper;
using Contracts.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SearchService.Consumers;

namespace AuctionService.Controllers;

[ApiController, Route("api/auctions")]
public class AuctionsController: ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuctionsController(AuctionDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint) {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions() {
        var auctions = await _context.Auctions.Include(x => x.Item)
                                     .OrderBy(x => x.Item.Make).ToListAsync();
        return _mapper.Map<List<AuctionDto>>(auctions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id) {
        var auction = await _context.Auctions.Include(x => x.Item).SingleOrDefaultAsync(x => x.Id == id);

        if (auction == null) return NotFound();

        return _mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto) {
        var auction = _mapper.Map<Auction>(auctionDto);
        //TODO: Add Current user as seller
        auction.Seller = "test";

        _context.Auctions.Add(auction);
        var mappedAuction = _mapper.Map<AuctionDto>(auction);
        await _publishEndpoint.Publish(_mapper.Map<AuctionCreatedContract>(mappedAuction));

        var result = await _context.SaveChangesAsync() > 0;

        

        if(!result) return BadRequest("Cannot save changes to DB");

        return CreatedAtAction(nameof(GetAuctionById),
                new { auction.Id }, mappedAuction);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto) {
        var auction = await _context.Auctions.Include(x => x.Item).SingleOrDefaultAsync(x => x.Id == id);
        if (auction == null) return NotFound();
        
        //TODO: Check seller = username;

        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;

        var mappedAuction = _mapper.Map<AuctionDto>(auction);
        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(mappedAuction));

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Problem saving changes");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id) {
        var auction = await _context.Auctions.FindAsync(id);
        if (auction == null) return NotFound();

        //TODO: 
        _context.Auctions.Remove(auction);

        var cahngeTracker = _context.ChangeTracker.Entries<Auction>();
        if (cahngeTracker.SingleOrDefault(x=>auction != null && x.Entity.Id == auction.Id)!.State == EntityState.Deleted)
        {
            var mappedAuction = _mapper.Map<AuctionDto>(auction);
            await _publishEndpoint.Publish<AuctionDeleted>(new {Id = auction.Id.ToString()});
        }

        var result = await _context.SaveChangesAsync() > 0;
        return result ? Ok() : BadRequest("Could not update the DB");
    }
}