////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: categoryIndex.js
// Description: loads all the category dynamically on the page
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
import { DOMCreator } from "./domCreator.js";
const categoryRepo = new CategoryRepository("https://localhost:7095/api");
const domCreator = new DOMCreator();

const CategoryTableBody = document.querySelector("#categoryTableBody");
CategoryTableBody.appendChild(domCreator.createImageTR("./images/ajax-loader.gif",
    "Loading image"));
let Category = await categoryRepo.readAll();
console.log(Category);
domCreator.removeChildren(CategoryTableBody);
Category.forEach((category) => {
    CategoryTableBody.appendChild(createCategoryTR(category));
});


//creates the category on the page 
function createCategoryTR(category) {
    const tr = document.createElement("tr");
    tr.appendChild(domCreator.createTextTD(category.id));
    tr.appendChild(domCreator.createTextTD(category.name));
    tr.appendChild(createTDWithLinks(category.id));

    return tr;
}

//populates action links on the categories
function createTDWithLinks(id) {
    const td = document.createElement("td");
    td.appendChild(domCreator.createTextLink(`/category/deletecat/${id}`, "Delete")); 
    td.appendChild(document.createTextNode(" | "));
    td.appendChild(domCreator.createTextLink(`/category/update/${id}`, "Update"));
    return td;
}