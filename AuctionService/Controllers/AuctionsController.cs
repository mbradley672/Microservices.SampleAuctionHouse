﻿using AuctionService.Data;
using AuctionService.Dtos;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController, Route("api/auctions")]
public class AuctionsController: ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;

    public AuctionsController(AuctionDbContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
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

        var result = await _context.SaveChangesAsync() > 0;

        return result ? CreatedAtAction(nameof(GetAuctionById), 
                new { auction.Id }, _mapper.Map<AuctionDto>(auction))
            : BadRequest("Cannot save changes to DB");
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

        var result = await _context.SaveChangesAsync() > 0;
        return result ? Ok() : BadRequest("Could not update the DB");
    }
}