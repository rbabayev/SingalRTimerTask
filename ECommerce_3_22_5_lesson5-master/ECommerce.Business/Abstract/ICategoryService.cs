using ECommerce.Entities.Models;

namespace ECommerce.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task Delete(int id);
        Task Add(Category category);

    }
}
