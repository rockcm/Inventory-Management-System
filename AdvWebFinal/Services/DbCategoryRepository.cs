////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: DbCategoryRepository.cs
// Description: class that interacts with the database to retrieve, edit and delete categories etc. 
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

    public class DbCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// injects the db context
        /// </summary>
        /// <param name="db">the db contxt </param>
        public DbCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// reads a category from the database
        /// </summary>
        /// <param name="id">the id of the category to be read</param>
        public async Task<Category?> ReadAsync(int id)
        {
            return await _db.Categories
                         .Include(c => c.CategoryProduct)
                         .ThenInclude(pc => pc.Product)
                         .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        /// <summary>
        /// reads all the categories from the database
        /// </summary>
        public async Task<ICollection<Category>> ReadAllAsync()
        {

            return await _db.Categories
                        .Include(c => c.CategoryProduct)
                        .ThenInclude(pc => pc.Product)
                        .ToListAsync();
        }

        /// <summary>
        /// creates a category 
        /// </summary>
        /// <param name="category">the category to be creates</param>
        public async Task<Category> CreateAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        /// <summary>
        /// deletes a category from the database 
        /// </summary>
        /// <param name="category"> the category to be deleted</param>
        public async Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// updates a category in the database
        /// </summary>
        /// <param name="cat">the categoy to be updated</param>
		public async Task<Category> UpdateAsync(Category cat)
		{
			var existingCat = await ReadAsync(cat.Id);

			if (existingCat == null)
			{
                throw new Exception("Category not found");
			}

			// Update properties of the existing product
			existingCat.Name = cat.Name;
			

			// Save changes to the database
			await _db.SaveChangesAsync();

			return existingCat;
		}

	}
}
