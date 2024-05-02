////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: IProductRepository.cs
// Description: interface class for ProductCategory 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///



using AdvWebFinal.Models.Entities;

namespace AdvWebFinal.Services
{
    public interface IProductCategoryRepository
    {

        Task<ICollection<ProductCategory>> ReadAllAsync();

        Task<ProductCategory?> ReadAsync(int id);
        Task<ProductCategory?> CreateAsync(int productId, int categoryId);
		Task RemoveAsync(int prodId, int catId);
	}
}
