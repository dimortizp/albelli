using AutoMapper;
using Core.Models;

namespace Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductType, Entities.ProductType>();
            CreateMap<OrderLine, Entities.OrderLine>()
                .ForMember(x => x.ProductType, opt => opt.Ignore());
            CreateMap<Order, Entities.Order>()
                .ForMember(x => x.OrderLines, opt => opt.Ignore());

            CreateMap<Entities.ProductType, ProductType>();
            CreateMap<Entities.OrderLine, OrderLine>();
            CreateMap<Entities.Order, Order>();
        }
    }
}