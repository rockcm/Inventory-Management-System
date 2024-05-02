////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: HomeController.cs
// Description: manages the action methods / views for the home controller. 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///
using AdvWebFinal.Models;
using AdvWebFinal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvWebFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepo;

        /// <summary>
        /// home controller constructor
        /// </summary>
        /// <param name="logger">logger </param>
        /// <param name="productRepository">the product repository</param>
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepo = productRepository;
        }

        /// <summary>
        /// view for the home page of the website
        /// </summary>
        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.ReadAllAsync();
            

            return View(products);
        }
        // privacy method
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
