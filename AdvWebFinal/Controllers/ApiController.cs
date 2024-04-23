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

        [HttpGet("products/{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("createproduct")]
        public async Task<IActionResult> Post([FromForm] Product product)
        {
             await _productRepo.CreateAsync(product);
            return CreatedAtAction("Get", new { id = product.Id }, product);
        }


        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] Product product)
        {
            await _productRepo.UpdateAsync(product);
            return NoContent(); // 204 as per HTTP specification
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
