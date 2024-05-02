////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ICategorytRepository.cs
// Description: interface class for category 
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
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> ReadAllAsync();

        Task<Category?> ReadAsync(int id);

		Task<Category> UpdateAsync(Category cat);
		Task<Category> CreateAsync(Category category);

        Task DeleteAsync(Category category);
    }
}
