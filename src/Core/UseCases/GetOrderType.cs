using System;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;

namespace Core.UseCases
{
    public class GetOrderType : IRequestHandler<string, ProductType>
    {
        private readonly IOrderTypeRepository _orderRepository;

        public GetOrderType(IOrderTypeRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<ProductType> HandleAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(name);

            return await _orderRepository.GetProductTypeAsync(name);
        }
    }
}