using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface IOrderTypeRepository
    {
        /// <summary>
        /// Get list of all productTypes
        /// </summary>
        /// <returns>IEnumerable of ProductType</returns>
        Task<IEnumerable<ProductType>> GetProductTypesAsync();

        /// <summary>
        /// Get ProductType based on Id
        /// </summary>
        /// <param name="name">name of productType</param>
        /// <returns>ProductType found or null</returns>
        Task<ProductType> GetProductTypeAsync(string name);
    }
}