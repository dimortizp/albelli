using System;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.UseCases
{
    public class CreateOrder : IRequestHandler<Order, int>
    {
        private readonly IOrderTypeRepository _orderTypeRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrder(
            IOrderTypeRepository orderTypeRepository, 
            IOrderRepository orderRepository)
        {
            _orderTypeRepository = orderTypeRepository ?? throw new ArgumentNullException(nameof(orderTypeRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<int> HandleAsync(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var orderId = await _orderRepository.CreateOrderAsync(order);
            if(order.OrderLines != null)
            {
                foreach(var orderLine in order.OrderLines)
                {
                    if (orderLine.Quantity <= 0) continue;
                    var orderType = await _orderTypeRepository.GetProductTypeAsync(orderLine.ProductType.Name);
                    orderLine.ProductTypeId = orderType.Id;
                    var _ = await _orderRepository.AddOrderLineToOrder(orderId, orderLine);
                }
            }
            return orderId;
        }
    }
}