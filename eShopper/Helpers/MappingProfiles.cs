using AutoMapper;
using eShopper.Core.Entities;
using eShopper.Core.Entities.Identity;
using eShopper.DTOs;

namespace eShopper.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
                .ReverseMap();


            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto,CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto,BasketItem>().ReverseMap();
        }
    }
}
