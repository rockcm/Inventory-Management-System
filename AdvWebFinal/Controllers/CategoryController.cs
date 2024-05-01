////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: CategoryController.cs
// Description: manages the action methods / views for the category controller. 
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
    public class CategoryController : Controller
    {
        //variables
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;


        /// <summary>
        /// Constructor for CategoryController class
        /// </summary>
        /// <param name="categoryRepo"> the category repository</param>
        /// <param name="productRepo">the productrepository</param>
        public CategoryController(ICategoryRepository categoryRepo, IProductRepository productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }


        /// <summary>
        /// Displays the index view for category
        /// </summary>
        public async Task<IActionResult> Index()
        {
            return View();
        }


        /// <summary>
        /// Displays the view for deleting a category with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        public async Task<IActionResult> DeleteCat(int id)
        {
            return View();
        }



        /// <summary>
        /// Displays the view for adding a category to a product.
        /// </summary>
        /// <param name="productId">The ID of the product that is being assigned the category.</param>
        public async Task<IActionResult> AddCategory(
        [Bind(Prefix = "id")] int productId)
        {
            var product = await _productRepo.ReadAsync(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }
            var allCategories = await _categoryRepo.ReadAllAsync();
            var categoriesAssigned = product.ProductCategory
                .Select(pc => pc.Category).ToList();
            var categoriesNotAssigned = allCategories.Except(categoriesAssigned);
            ViewData["Product"] = product;
            return View(categoriesNotAssigned);
        }


        /// <summary>
        /// displays the view for creating a new category.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

      
     



    }
}
