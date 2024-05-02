////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: productCreate.js
// Description: class to create a product in the databse using javascript
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///

"use strict"
import { ProductRepository } from "./ProductRepository.js";


const productRepo = new ProductRepository("https://localhost:7095/api");

const productCreateForm = document.querySelector("#formCreateProduct");
productCreateForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(productCreateForm);
    const result = await productRepo.create(formData);
    console.log(result);
    window.location.replace("/product/index");
});