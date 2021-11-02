using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.UseCases
{
    public class GetAllOrderTypes : IRequestHandler<IEnumerable<ProductType>>
    {
        private readonly IOrderTypeRepository _orderRepository;

        public GetAllOrderTypes(IOrderTypeRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<IEnumerable<ProductType>> HandleAsync()
        {
            return await _orderRepository.GetProductTypesAsync();
        }
    }
}