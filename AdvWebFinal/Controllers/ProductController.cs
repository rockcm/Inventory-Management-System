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
        private readonly IProductCategoryRepository _productCategoryRepo;



        public ProductController(IProductRepository productRepo, IProductCategoryRepository productCategoryRepo)
        {
            _productRepo = productRepo;
            _productCategoryRepo = productCategoryRepo;
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


        public async Task<IActionResult> Remove(
        [Bind(Prefix = "id")] int prodId, int catId)
        {
            var product = await _productRepo.ReadAsync(prodId);
            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }
            var prodCat = product.ProductCategory
                .FirstOrDefault(pc => pc.CategoryId == catId);
            if (prodCat == null)
            {
                return RedirectToAction("Details", "Product", new { id = prodId });
            }
            return View(prodCat);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
        public async Task<IActionResult> RemoveConfirmed(
            int prodId, int catId)
        {
            await _productCategoryRepo.RemoveAsync(prodId, catId);
            return RedirectToAction("Details", "Student", new { id = prodId });
        }

    }
}
