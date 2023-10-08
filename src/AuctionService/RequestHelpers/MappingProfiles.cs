using AuctionService.Dtos;
using AuctionService.Entities;
using AutoMapper;
using Contracts.Contracts;

namespace AuctionService.RequestHelpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(m => m.Item);
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>().ForMember(m => m.Item, o => o.MapFrom(e => e));
        CreateMap<CreateAuctionDto, Item>();
        CreateMap<AuctionDto, AuctionCreatedContract>();
        CreateMap<Item,AuctionUpdated>();
        CreateMap<AuctionDto, AuctionUpdated>();
        CreateMap<Auction, AuctionUpdated>().IncludeMembers(m => m.Item);

    }
}