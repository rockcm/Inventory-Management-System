////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ApplicationDbContext.cs
// Description: class that interacts with the database and sets up database tables. 
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
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// initializes the db context options
        /// </summary>
        /// <param name="options"> the db context option</param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        /// <summary>
        /// allows the entities to be decimals in the database
        /// </summary>
        /// <param name="modelBuilder"> the model builder // defines entities</param>
        /// found info about how to do here
        /// https://github.com/dotnet/efcore/issues/29313 
        /// https://stackoverflow.com/questions/43096005/devart-oracle-9-3-entity-framework-7-or-core-1-mapping-cant-recognize-decim"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.SellPrice)
                .HasColumnType("decimal(18, 2)");  // added to allow decimals

            modelBuilder.Entity<Product>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18, 2)"); // added to allow decimals
        }

      

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        
    }
}
