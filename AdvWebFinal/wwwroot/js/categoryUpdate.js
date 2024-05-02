////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: categoryUpdate.js
// Description: javascript for the update category page 
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

const categoryHeading = document.querySelector("#categoryHeading");
domCreator.removeChildren(categoryHeading);
categoryHeading.appendChild(
    domCreator.createImg("/images/ajax-loader.gif", "Loading image"));
const urlSections = window.location.href.split("/");
const categoryId = urlSections[5];
console.log(categoryId)
await populateCategoryData();
const formCategoryEdit = document.querySelector("#formCategoryEdit");
formCategoryEdit.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(formCategoryEdit);
    try {
        await categoryRepo.update(formData);
        window.location.replace("/category/index/");
    }
    catch (error) {
        console.log(error);
    }
});

// method to populate the category data from the database
async function populateCategoryData() {
    try {
        const category = await categoryRepo.read(categoryId);
        console.log(category);
        domCreator.setElementValue("#Id", category.id);
        domCreator.setElementValue("#Name", category.name);
       
        domCreator.removeChildren(categoryHeading);
        categoryHeading.appendChild(document.createTextNode("category"));
    }
    catch (error) {
        console.log(error);
        window.location.replace("/category/index");
    }
}