using AdvWebFinal.Models.Entities;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductCategoryRepository _productCategoryRepo;

        public ApiController(IProductRepository productRepo, ICategoryRepository categoryRepo, IProductCategoryRepository productCategoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _productCategoryRepo = productCategoryRepo;
        }

        // Product CRUD operations

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepo.ReadAllAsync();
            return Ok(products);
        }

        [HttpPost("createproduct")]
        public IActionResult Post([FromForm] Product product)
        {
            _productRepo.CreateAsync(product);
            return CreatedAtAction("Get", new { id = product.Id }, product);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepo.ReadAllAsync();
            return Ok(categories);
        }

     
        [HttpGet("productcategories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            var productCategories = await _productCategoryRepo.ReadAllAsync();
            return Ok(productCategories);
        }

     
    }
}
