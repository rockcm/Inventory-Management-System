using AdvWebFinal.Models.Entities;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
       
       

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
           
        }

        public async Task< IActionResult> Index()
        {
            return View(await _productRepo.ReadAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            if (product == null)
            {
                await Console.Out.WriteLineAsync("product was null error");
                return RedirectToAction("Index");
            }
            return View(product);
        }

     
        //done with js
        public IActionResult Create()
        {
            return View();
        }

        //done with js

        public IActionResult Update()
        {
            return View();
        }

        /// <summary>
        /// Delete action method 
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        /// <summary>
        /// Delete post action method 
        /// </summary>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           // await _productRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.SearchTerm = searchTerm;
                return View(new List<Product>()); // Return an empty list if search term is empty
            }

            // Call the SearchProductsAsync method from the ProductRepository
            var searchResults = await _productRepo.SearchProductsAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(searchResults); // Return the search results to the view
        }

     
    }
}
