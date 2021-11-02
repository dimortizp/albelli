using System;
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
    public class OrderTypeRepository : IOrderTypeRepository
    {
        private readonly OrderManagerContext _ctx;
        private readonly IMapper _mapper;

        public OrderTypeRepository(OrderManagerContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<ProductType> GetProductTypeAsync(string name)
        {
            var productType = await _ctx.ProductTypes.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
            return productType != null ? _mapper.Map<ProductType>(productType) : null;
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            return from p in await _ctx.ProductTypes.ToListAsync()
                select _mapper.Map<ProductType>(p);
        }
    }
}