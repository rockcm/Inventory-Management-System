////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: categoryCreate.js
// Description: class to create a category in the databse using javascript
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///


"use strict"
import { CategoryRepository } from "./CategoryRepository.js";


const categoryRepo = new CategoryRepository("https://localhost:7095/api");

const categoryCreateForm = document.querySelector("#formCreateCategory");
categoryCreateForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(categoryCreateForm);
    const result = await categoryRepo.create(formData);
    console.log(result);
    window.location.replace("/category/index");
});