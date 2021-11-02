using AutoMapper;
using Core.Models;

namespace Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductType, Entities.ProductType>();
            CreateMap<OrderLine, Entities.Order>();
            CreateMap<Order, Entities.Order>();

            CreateMap<Entities.ProductType, ProductType>();
            CreateMap<Entities.OrderLine, Order>();
            CreateMap<Entities.Order, Order>();
        }
    }
}