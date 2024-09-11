using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstraction;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IFilterService _filterService;

        public ProductService(IProductDal productDal, IFilterService filterService)
        {
            _productDal = productDal;
            _filterService = filterService;
        }

        public async Task AddAsync(Product product)
        {
            await _productDal.Add(product);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _productDal.Get(p => p.ProductId == id);
            await _productDal.Delete(item);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _productDal.GetList();
        }

        public Task<List<Product>> GetAllByCategoryAsync(int categoryId)
        {
            return _productDal.GetList(p => p.CategoryId == categoryId || categoryId == 0);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productDal.Get(p => p.ProductId == id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productDal.Update(product);
        }

        public async Task<List<Product>> GetFilteredProductsAsync(int categoryId, string sortBy)
        {
            var products = await GetAllByCategoryAsync(categoryId);

            switch (sortBy)
            {
                case "az":
                    products = await _filterService.FilterByName(products, true);
                    break;
                case "za":
                    products = await _filterService.FilterByName(products, false);
                    break;
                case "priceLowToHigh":
                    products = await _filterService.FilterByPrice(products, true);
                    break;
                case "priceHighToLow":
                    products = await _filterService.FilterByPrice(products, false);
                    break;
                default:
                    break;
            }

            return products;
        }
    }
}
