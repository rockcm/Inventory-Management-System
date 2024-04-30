////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System
// File Name: ProductController.CS
// Description: Product controller class that provides views for the product pages. 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024
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

     
       
        public IActionResult Create()
        {
            return View();
        }

       

        public IActionResult Update()
        {
            return View();
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }


        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.SearchTerm = searchTerm;
                return View(new List<Product>()); // Returns an empty list if search term is empty
            }

            // Call the Search method from the ProductRepository
            var searchResults = await _productRepo.SearchProductsAsync(searchTerm);
            ViewBag.SearchTerm = searchTerm;
            return View(searchResults); // Returns the search results to the view
        }

     
    }
}
