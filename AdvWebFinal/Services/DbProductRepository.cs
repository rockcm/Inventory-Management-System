////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: DbProductRepository.cs
// Description: class that interacts with the database to retrieve, edit and delete products etc. 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///


using AdvWebFinal.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdvWebFinal.Services
{
    public class DbProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// injects the db context 
        /// </summary>
        /// <param name="db">the db context</param>
        public DbProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // returns all products from the database
        public async Task<ICollection<Product>> ReadAllAsync()
        {
            var products = await _db.Products
                .Include(p => p.ProductCategory)
                    .ThenInclude(pc => pc.Category)
                .ToListAsync();

            // create a projection for the products
            return products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                SellPrice = p.SellPrice,
                PurchasePrice = p.PurchasePrice,
                Stock = p.Stock,
                Image = p.Image,
                ProductCategory = p.ProductCategory.Select(pc => new ProductCategory
                {
                    CategoryId = pc.CategoryId,
                    Category = new Category
                    {
                        
                        Name = pc.Category.Name
                       
                    }
                }).ToList()
            }).ToList();
        }

        /// <summary>
        /// returns product with corresponding Id
        /// </summary>
        /// <param name="id">the id of the item to be read</param>
        /// <returns></returns>
        public async Task<Product?> ReadAsync(int id)
        {

            return await _db.Products
            .Include(p => p.ProductCategory)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        }

        /// <summary>
        ///  creates a product in the database
        /// </summary>
        /// <param name="product">the product to be created</param>
        public async Task<Product> CreateAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// updates a product in the database 
        /// </summary>
        /// <param name="product">the product to be updated</param>
        public async Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = await ReadAsync(product.Id);

            if (existingProduct == null)
            {
                throw new ArgumentException($"Product with ID {product.Id} not found.");
            }

            // Update properties of the existing product
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.SellPrice = product.SellPrice;
            existingProduct.PurchasePrice = product.PurchasePrice;
            existingProduct.Stock = product.Stock;
            existingProduct.Image = product.Image;

            // Save changes to the database
            await _db.SaveChangesAsync();

            return existingProduct;
        }

        /// <summary>
        /// deletes a product from the database
        /// </summary>
        /// <param name="product">the product that will be deleted</param>
        public async Task DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// searche database for products that contain the string search term 
        /// </summary>
        /// <param name="searchTerm">the words/characters to be searched</param>
        /// <returns>all products that contain the search term </returns>
        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            // searches through db for products containing search term
            return await _db.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }

    

  
 

    }
}
