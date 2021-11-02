using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderManagerContext _ctx;
        private readonly IMapper _mapper;

        public OrderRepository(OrderManagerContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            return from p in await _ctx.ProductTypes.ToListAsync()
                select _mapper.Map<ProductType>(p);
        }
    }
}