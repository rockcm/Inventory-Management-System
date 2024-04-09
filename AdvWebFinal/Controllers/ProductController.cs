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



    }
}
