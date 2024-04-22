﻿using AdvWebFinal.Models.Entities;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvWebFinal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;

        public CategoryController(ICategoryRepository categoryRepo, IProductRepository productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepo.ReadAllAsync());
        }

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

        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepo.CreateAsync(category);
                return RedirectToAction("Index", "category");
            }
            return View(category);
        }



    }
}
