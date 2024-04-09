﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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

        public ICollection<ProductCategory> ProductCategory { get; set; } = new List<ProductCategory>();
    }
}
