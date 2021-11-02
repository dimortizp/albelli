using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order);

        Task<Order> GetOrderAsync(int id);

        Task<int> AddOrderLineToOrder(int orderId, OrderLine orderLine);
    }
}