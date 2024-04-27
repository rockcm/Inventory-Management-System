﻿using AdvWebFinal.Models.Entities;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
              var products = await _productRepo.ReadAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while retrieving products: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
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

        [HttpDelete("delete/{id}")]
        public async Task Delete([FromForm] int id)
        {
           
            await _productRepo.DeleteAsync(id);
             NoContent(); // 204 as per HTTP specification
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
