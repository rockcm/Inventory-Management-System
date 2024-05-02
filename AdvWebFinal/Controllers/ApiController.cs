////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: APIController.cs
// Description: Controls all api data for product, category and productcategoryrepos 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///

using AdvWebFinal.Models.Entities;
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

        /// <summary>
        /// constructor for the api controller
        /// </summary>
        /// <param name="categoryRepo"> the category repository</param>
        /// <param name="productRepo">the product repository</param>
        /// <param name="productCategoryRepo">the productcategory repository</param>
        public ApiController(IProductRepository productRepo, ICategoryRepository categoryRepo, IProductCategoryRepository productCategoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _productCategoryRepo = productCategoryRepo;
        }

       /// <summary>
       /// contains all json information for products
       /// </summary>
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


        /// <summary>
        /// contains the json information for product with the id
        /// </summary>
        /// <param name="id">the id for product</param>
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

        /// <summary>
        /// creates a product in the database 
        /// </summary>
        /// <param name="product">the product to be created</param>
        /// <returns></returns>
        [HttpPost("createproduct")]
        public async Task<IActionResult> Post([FromForm] Product product)
        {
             await _productRepo.CreateAsync(product);
            return CreatedAtAction("Get", new { id = product.Id }, product);
        }


        /// <summary>
        /// delests the product information for product with the id from the database
        /// </summary>
        /// <param name="id">the id for product</param>
        [HttpDelete("product/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productToDelete = await _productRepo.ReadAsync(id);

            if (productToDelete == null)
            {
                return NotFound(); // Return 404 if product not found
            }

            await _productRepo.DeleteAsync(productToDelete);

            return NoContent(); // Return 204 as per HTTP specification
        }


        /// <summary>
        /// udpates a product in the database
        /// </summary>
        /// <param name="product">the product to be updated</param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Put([FromForm] Product product)
        {
            await _productRepo.UpdateAsync(product);
            return NoContent(); // 204 as per HTTP specification
        }


        /// <summary>
        /// contains all json information for categories
        /// </summary>
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepo.ReadAllAsync();
            return Ok(categories);
        }


        /// <summary>
        /// contains the json information for category with the id
        /// </summary>
        /// <param name="id">the id for category</param>
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<Product>> GetCat(int id)
        {
            var cat = await _categoryRepo.ReadAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }

        /// <summary>
        /// delests the product information for category with the id from the database
        /// </summary>
        /// <param name="id">the id for category</param>
        [HttpDelete("category/delete/{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            var catToDelete = await _categoryRepo.ReadAsync(id);

            if (catToDelete == null)
            {
                return NotFound(); // Return 404 if product not found
            }

            await _categoryRepo.DeleteAsync(catToDelete);

            return NoContent(); // Return 204 as per HTTP specification
        }


        /// <summary>
        /// contains all the product category json information
        /// </summary>

        [HttpGet("productcategories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            var productCategories = await _productCategoryRepo.ReadAllAsync();
            return Ok(productCategories);
        }


        /// <summary>
        /// creates a category in the database 
        /// </summary>
        /// <param name="category">the category to be created</param>
        [HttpPost("createcategory")]
        public async Task<IActionResult> Post([FromForm] Category category)
        {
            await _categoryRepo.CreateAsync(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }

        /// <summary>
        /// updates a category 
        /// </summary>
        /// <param name="category">the category to be updates</param>
        /// <returns></returns>
        [HttpPut("updatecategory")]
        public async Task<IActionResult> Put([FromForm] Category cat)
        {
            await _categoryRepo.UpdateAsync(cat);
            return NoContent(); // 204 as per HTTP specification
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId">the product id</param>
        /// <param name="catId">the category id</param>
        /// <returns></returns>
        [HttpPost("createproductcategory")]
        public async Task<IActionResult> PostAsync([FromForm] int productId, [FromForm] int catId)
        {
            var productCategory = await _productCategoryRepo.CreateAsync(productId, catId);
            // Remove the circular reference for the JSON
            productCategory?.Product?.ProductCategory.Clear();
            productCategory?.Category?.CategoryProduct.Clear();
            return CreatedAtAction("Get",
                new { id = productCategory?.Id }, productCategory);
        }

        /// <summary>
        /// removes a product from a category NOT IMPLEMENTED 
        /// </summary>
        /// <param name="productId">the product Id</param>
        /// <param name="catId">the category id</param>
        /// <returns></returns>
        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteAsync(
        [FromForm] int productId,
        [FromForm] int catId)
        {
            await _productCategoryRepo.RemoveAsync(productId, catId);
            return NoContent(); // 204 as per HTTP specification
        }


    }
}
