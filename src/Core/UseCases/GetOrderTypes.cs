using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.UseCases
{
    public class GetOrderTypes : IRequestHandler<IEnumerable<ProductType>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderTypes(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<IEnumerable<ProductType>> HandleAsync()
        {
            return await _orderRepository.GetProductTypesAsync();
        }
    }
}