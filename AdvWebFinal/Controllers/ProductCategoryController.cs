﻿////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ProductCategoryController.cs
// Description: manages the action methods / views for the productcategory controller. 
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
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{

    public class ProductCategoryController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductCategoryRepository _productCategoryRepo;

        /// <summary>
        /// controller constructor that injects product and productcategory repositories
        /// </summary>
        /// <param name="productRepo">the product repository</param>
        /// <param name="productCategoryRepo">the productcategory repository</param>
        ///<param name="categoryRepo"> the category repository</param>
        public ProductCategoryController(IProductCategoryRepository productCategoryRepo,IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _productCategoryRepo = productCategoryRepo;


        }

        /// <summary>
        ///  returns the index view for productcategory
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// returns the page where the product is assigned to a category
        /// </summary>
        ///  <param name="productId">the products id</param>
        /// <param name="categoryId">the categories id</param>
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

        /// <summary>
        /// post method for adding category to product
        /// </summary>
        /// <param name="productId">the products id</param>
        /// <param name="categoryId">the categories id</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("CreateConfirmed")]
        public async Task<IActionResult> CreateConfirmed(int productId, int categoryId)
        {
            await _productCategoryRepo.CreateAsync(productId, categoryId);
            return RedirectToAction("Details", "Product", new { id = productId });
        }

        /// <summary>
        /// returns the page where the product is removed from a category
        /// </summary>
        /// <param name="productId">the products id</param>
        /// <param name="categoryId">the categories id</param>
        /// <returns></returns>
		public async Task<IActionResult> Remove(
	   int productId, int categoryId)
		{
			var product = await _productRepo.ReadAsync(productId);
			if (product == null)
			{
				return RedirectToAction("Index", "Product");
			}
			var prodCat = product.ProductCategory
				.FirstOrDefault(pc => pc.CategoryId == categoryId);
			if (prodCat == null)
			{
				return RedirectToAction("Details", "Product", new { id = productId });
			}
			return View(prodCat);
		}

        /// <summary>
        /// post method for removing category to product
        /// </summary>
        /// <param name="productId">the products id</param>
        /// <param name="categoryId">the categories id</param>
        /// <returns></returns>
		[HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
		public async Task<IActionResult> RemoveConfirmed(
			int productId, int categoryId)
		{
			await _productCategoryRepo.RemoveAsync(productId, categoryId);
			return RedirectToAction("Details", "Product", new { id = productId });
		}



	}
}
