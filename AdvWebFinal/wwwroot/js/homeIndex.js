////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ProductIndex.js
// Description: loads all the products dynamically on the page // similar to productindex.js
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

const productTableBody = document.querySelector("#productTableBody");
productTableBody.appendChild(domCreator.createImageTR("./images/ajax-loader.gif",
    "Loading image"));



// self invoking function to automatically load 3 products 
(async () => {
    let products = await productRepo.readAll();
    console.log(products);

    // Shuffle the products randomly
    products = shuffle(products);

    const randomProducts = products.slice(0, 3);

    // Clear existing content from the table body
    domCreator.removeChildren(productTableBody);

    // Populate the table with the selected random products
    randomProducts.forEach((product) => {
        productTableBody.appendChild(createProductTR(product));
    });
})();


function createProductTR(product) {
    const tr = document.createElement("tr");
    tr.appendChild(domCreator.createTextTD(product.id));
    tr.appendChild(domCreator.createTextTD(product.name));
    tr.appendChild(domCreator.createTextTD(product.description));
    tr.appendChild(domCreator.createTextTD(product.purchasePrice));
    tr.appendChild(domCreator.createTextTD(product.sellPrice));
    tr.appendChild(domCreator.createTextTD(product.stock));
    tr.appendChild(domCreator.createImageTD(product.image));
    tr.appendChild(createTDWithLinks(product.id));
   
    return tr;
}
function createTDWithLinks(id) {
    const td = document.createElement("td");
    td.appendChild(domCreator.createTextLink(`/product/update/${id}`, "Edit"));
    td.appendChild(document.createTextNode(" | "));
    td.appendChild(domCreator.createTextLink(`/product/details/${id}`, "Details"));
    td.appendChild(document.createTextNode(" | "));
    td.appendChild(domCreator.createTextLink(`/product/delete/${id}`, "Delete"));
    return td;
}

//https://medium.com/@khaledhassan45/how-to-shuffle-an-array-in-javascript-6ca30d53f772
function shuffle(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]];
    }
    return array
}