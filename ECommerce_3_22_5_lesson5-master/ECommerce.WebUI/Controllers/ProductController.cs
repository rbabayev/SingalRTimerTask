using ECommerce.Business.Abstract;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFilterService _filterService;
        public ProductController(IProductService productService, IFilterService filterService)
        {
            _productService = productService;
            _filterService = filterService;


        }

        // GET: ProductController
        public async Task<ActionResult> Index(int page = 1, int category = 0, string sort = "")
        {

            var items = await _productService.GetAllByCategoryAsync(category);
            int pageSize = 10;


            var products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            if (sort == "a-z") { products = await _filterService.FilterByName(products, true); }
            else if (sort == "z-a") { products = await _filterService.FilterByName(products, false); }
            else if (sort == "HighToLow") { products = await _filterService.FilterByPrice(products, true); }
            else if (sort == "LowToHigh") { products = await _filterService.FilterByPrice(products, false); }


            var model = new ProductListViewModel
            {
                Products = products,
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize),
                CurrentSort = sort

            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SearchProducts(string searchTerm, int page = 1, int category = 0)
        {
            var items = await _productService.GetAllByCategoryAsync(category);

            if (!string.IsNullOrEmpty(searchTerm)) items = items.Where(p => p.ProductName.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            int pageSize = 10;
            var model = new ProductListViewModel
            {
                Products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageSize = pageSize,
                CurrentCategory = category,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize)
            };
            return PartialView("_ProductList", model);

        }



        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
