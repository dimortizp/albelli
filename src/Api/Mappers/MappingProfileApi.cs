using Api.Models;
using AutoMapper;
using Core.Models;

namespace Api.Mappers
{
    public class MappingProfileApi : Profile
    {
        public MappingProfileApi()
        {
            CreateMap<ProductTypeRequest, ProductType>();
            CreateMap<OrderLineRequest, OrderLine>();
            CreateMap<OrderRequest, Order>();


            CreateMap<ProductType, ProductTypeResponse>();
            CreateMap<OrderLine, OrderLineResponse>();
            CreateMap<Order, OrderResponse>();
        }
    }
}