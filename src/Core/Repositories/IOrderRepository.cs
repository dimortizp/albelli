using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<ProductType>> GetProductTypesAsync();
    }
}