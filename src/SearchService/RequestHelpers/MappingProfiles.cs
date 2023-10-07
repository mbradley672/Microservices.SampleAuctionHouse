using AutoMapper;
using Contracts.Contracts;
using SearchService.Entities;

namespace SearchService.RequestHelpers {
    public class MappingProfiles: Profile {
        public MappingProfiles() {
            CreateMap<AuctionCreatedContract, Item>();
            CreateMap<AuctionUpdated, Item>();
        }
    }
}
