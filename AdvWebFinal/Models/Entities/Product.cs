////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: Product.cs
// Description: model for products
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace AdvWebFinal.Models.Entities
{
    public class Product
    {


        public int Id { get; set; }
        public string? Name { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
        [DisplayName("Sell Price")]
        public decimal? SellPrice { get; set; }
        [DisplayName("Purchase Price")]
        public decimal PurchasePrice { get; set; }
        public int Stock {  get; set; }

        public string? Image { get; set; }

		[JsonIgnore]
		public ICollection<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();



    }
}
