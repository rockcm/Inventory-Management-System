using AdvWebFinal.Models;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvWebFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepo;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepo = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.ReadAllAsync();
            

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
