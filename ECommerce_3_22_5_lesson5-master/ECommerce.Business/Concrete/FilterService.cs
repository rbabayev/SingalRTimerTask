using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class FilterService : IFilterService
    {
        public async Task<List<Product>> FilterByName(IEnumerable<Product> products, bool filter)
        {
            return await Task.FromResult(filter ? products.OrderBy(p => p.ProductName).ToList() : products.OrderByDescending(p => p.ProductName).ToList());
        }

        public async Task<List<Product>> FilterByPrice(IEnumerable<Product> products, bool filter)
        {
            return await Task.FromResult(filter ? products.OrderBy(p => p.UnitPrice).ToList() : products.OrderByDescending(p => p.UnitPrice).ToList());
        }
    }
}
