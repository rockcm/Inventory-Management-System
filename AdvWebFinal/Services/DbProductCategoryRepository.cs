////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: DbCategoryRepository.cs
// Description: class that interacts with the database to retrieve, edit and delete productcategories etc. 
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

namespace AdvWebFinal.Services
{
    public class DbProductCategoryRepository : IProductCategoryRepository
    {


        //variables
        private readonly ApplicationDbContext _db;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IProductRepository _productRepo;

        /// <summary>
        /// constuctor from the DbProductCategoryRepository, injects the dbcontext, productrepo and category rep 
        /// </summary>
        /// <param name="db">the database context</param>
        /// <param name="productRepo">the product repository</param>
        /// <param name="categoryRepo">the category repository</param>
        public DbProductCategoryRepository(ApplicationDbContext db, IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _db = db;
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        /// <summary>
        /// reads the product category from the database
        /// </summary>
        /// <param name="id">the id for the product category</param>
        public async Task<ProductCategory?> ReadAsync(int id)
        {
            return await _db.ProductCategories
               .Include(pc => pc.Product)
               .Include(pc => pc.Category)
               .FirstOrDefaultAsync(pc => pc.Id == id);
        }


        /// <summary>
        /// reads all the productcategories
        /// </summary>
        public async Task<ICollection<ProductCategory>> ReadAllAsync()
        {
            return await _db.ProductCategories 
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
               .ToListAsync();
        }

        /// <summary>
        /// Assigns the product a category. creates a product category using the productId and the categoryId
        /// </summary>
        /// <param name="productId">the product id</param>
        /// <param name="categoryId">the category id</param>
        /// <returns></returns>
        public async Task<ProductCategory> CreateAsync(int productId, int categoryId)
        {
            
            var product = await _productRepo.ReadAsync(productId);
            if (product == null)
            {
                //product not found
                return null;
            }

            var category = await _categoryRepo.ReadAsync(categoryId);
            if (category == null)
            {
                //category not found 
                return null;
            }
         
            var existingProductCategory = product.ProductCategory.FirstOrDefault(pc => pc.CategoryId == categoryId);
            if (existingProductCategory != null)
            {
              
                //if productcategory is already in db
                return null;
            }

            var productCategory = new ProductCategory
            {
                Product = product,
                Category = category
            };
    
            product.ProductCategory.Add(productCategory);
            category.CategoryProduct.Add(productCategory);

          
            await _db.SaveChangesAsync();

            return productCategory;
        }

        /// <summary>
        /// removes a category from a product
        /// </summary>
        /// <param name="prodId">the product id</param>
        /// <param name="CatId">the category id </param>
        public async Task RemoveAsync(int prodId, int CatId)
        {
            var product = await _productRepo.ReadAsync(prodId);

            var prodCat = await _db.ProductCategories
                .Include(pc => pc.Category)
                .FirstOrDefaultAsync(pc => pc.ProductId == prodId && pc.CategoryId == CatId); 

            if (prodCat == null)
            {
                throw new Exception($"ProductCategory with product ID {prodId} and category ID {CatId} not found.");
            }

            var cat = prodCat.Category;

            product.ProductCategory.Remove(prodCat);
            cat.CategoryProduct.Remove(prodCat);

            await _db.SaveChangesAsync();
        }


    }


}
