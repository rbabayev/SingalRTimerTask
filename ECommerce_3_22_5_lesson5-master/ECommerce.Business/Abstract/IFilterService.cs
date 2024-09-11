using ECommerce.Entities.Models;

namespace ECommerce.Business.Abstract
{
    public interface IFilterService
    {
        Task<List<Product>> FilterByName(IEnumerable<Product> products, bool filter);
        Task<List<Product>> FilterByPrice(IEnumerable<Product> products, bool filter);
    }
}
