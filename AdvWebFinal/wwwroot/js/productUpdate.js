////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: productUpdate.js
// Description: javascript for the update product page 
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
import { DOMCreator } from "./domCreator.js";
const productRepo = new ProductRepository("https://localhost:7095/api");
const domCreator = new DOMCreator();

const productHeading = document.querySelector("#productHeading");
domCreator.removeChildren(productHeading);
productHeading.appendChild(
    domCreator.createImg("/images/ajax-loader.gif", "Loading image"));
const urlSections = window.location.href.split("/");
const productId = urlSections[5];
await populateProductData();
const formProductEdit = document.querySelector("#formProductEdit");
formProductEdit.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(formProductEdit);
    try {
        await productRepo.update(formData);
        window.location.replace("/product/index/");
    }
    catch (error) {
        console.log(error);
    }
});



// function to populate the product data from the database
async function populateProductData() {
    try {
        const product = await productRepo.read(productId);
        console.log(product);
        domCreator.setElementValue("#Id", product.id);
        domCreator.setElementValue("#Name", product.name);
        domCreator.setElementValue("#Description", product.description);
        domCreator.setElementValue("#PurchasePrice", product.purchasePrice);
        domCreator.setElementValue("#SellPrice", product.sellPrice);
        domCreator.setElementValue("#Stock", product.stock);
        domCreator.setElementValue("#Image", product.image);
        domCreator.removeChildren(productHeading);
        productHeading.appendChild(document.createTextNode("Product"));
    }
    catch (error) {
        console.log(error);
        window.location.replace("/product/index");
    }
}