////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ProductController.CS
// Description: Product controller class that provides action methods and views for the product pages. 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////

using AdvWebFinal.Models.Entities;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly IProductCategoryRepository _productCategoryRepo;


        /// <summary>
        /// controller constructor that injects product and productcategory repositories
        /// </summary>
        /// <param name="productRepo">the product repository</param>
        /// <param name="productCategoryRepo">the product repository</param>
        public ProductController(IProductRepository productRepo, IProductCategoryRepository productCategoryRepo)
        {
            _productRepo = productRepo;
            _productCategoryRepo = productCategoryRepo;
        }

        /// <summary>
        ///  returns the index view
        /// </summary>
        public async Task< IActionResult> Index()
        {

            return View(_productRepo.ReadAllAsync());


        }

        /// <summary>
        /// returns the view with the product 
        /// </summary>
        /// <param name="id">the id for the product read from database</param>
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
        // not used ingore this and view 
        public async Task<IActionResult> DetailsPlus(int id)
        {
            var product = await _productRepo.ReadAsync(id);
            if (product == null)
            {
                await Console.Out.WriteLineAsync("product was null error");
                return RedirectToAction("Index");
            }
            return View(product);
        }

        /// <summary>
        /// returns the create page for the product
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// returns the update page for the product
        /// </summary>
        public IActionResult Update()
        {
            return View();
        }


        /// <summary>
        /// returns the delete page for the product
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        /// <summary>
        /// searches the database for the word or characters entered 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns>shows all the products with the descr</returns>
        /// 
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.SearchTerm = searchTerm;
                return View(new List<Product>()); // returns empty list if no products found
            }

            
            var searchResults = await _productRepo.SearchProductsAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm; // allows for search term to be used dynamically in view.. 
            return View(searchResults); // Returns the search results to the view
        }


     


    }
}
