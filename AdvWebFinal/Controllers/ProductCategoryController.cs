﻿using AdvWebFinal.Models.Entities;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductCategoryRepository _productCategoryRepo;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepo,IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _productCategoryRepo = productCategoryRepo;


        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int productId, int categoryId)
        {
            // Retrieve the product and category
            var product = await _productRepo.ReadAsync(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }

            var category = await _categoryRepo.ReadAsync(categoryId);
            if (category == null)
            {
                // Redirect to the product details page if the category is not found
                return RedirectToAction("Details", "Product", new { id = productId });
            }

            // Create a new ProductCategory instance
            var productCategory = new ProductCategory
            {
                Product = product,
                Category = category
            };

            // Add the product category to the product and category collections
            product.ProductCategory.Add(productCategory);
            category.CategoryProduct.Add(productCategory);

     
            // Redirect to the product details page
            return View(productCategory);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(int productId, int categoryId)
        {
            await _productCategoryRepo.CreateAsync(productId, categoryId);
            return RedirectToAction("Details", "Product", new {id  = productId});
        }

    }
}
