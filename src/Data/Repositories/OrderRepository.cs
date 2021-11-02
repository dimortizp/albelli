using AutoMapper;
using Core.Models;
using Core.Repositories;
using Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> AddOrderLineToOrder(int orderId, OrderLine orderLine)
        {
            var orderLineSaved =
                _mapper.Map<Entities.OrderLine>
                (
                    orderLine,
                    opt => opt.AfterMap
                    (
                        (src, dest) => dest.OrderId = orderId
                    )
                 );
            _ctx.OrderLines.Add(orderLineSaved);
            await _ctx.SaveChangesAsync();
            return orderLineSaved.Id;
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            var orderSaved = _mapper.Map<Entities.Order>(order);
            _ctx.Orders.Add
                (
                    orderSaved
                );
            await _ctx.SaveChangesAsync();
            return orderSaved.Id;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var order = _ctx.Orders.Where(o => o.Id == id)
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductType)
                .FirstOrDefault();

            return await Task.FromResult(_mapper.Map<Order>(order));
        }
    }
}
