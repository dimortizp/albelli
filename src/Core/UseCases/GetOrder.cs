using System;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.UseCases
{
    public class GetOrder : IRequestHandler<int,Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrder(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<Order> HandleAsync(int orderId)
        {
            return await _orderRepository.GetOrderAsync(orderId);
        }
    }
}