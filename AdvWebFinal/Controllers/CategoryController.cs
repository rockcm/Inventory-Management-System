using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;

        public CategoryController(ICategoryRepository categoryRepo, IProductRepository productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepo.ReadAllAsync());
        }

        public async Task<IActionResult> AddCategory(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }

            var allCategories = await _categoryRepo.ReadAllAsync(); // Assuming you have a method to read all categories

            // Pass the product and available categories to the view using ViewData
            ViewData["Product"] = product;
            ViewData["AllCategories"] = allCategories;

            return View();
        }




    }
}
