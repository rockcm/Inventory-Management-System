////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: categoryDelete.js
// Description: class  to deletes a product from the databse using javascript
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
categoryHeading.appendChild(document.createTextNode("Loading..."));
const urlSections = window.location.href.split("/");
const categoryId = urlSections[5];
await populateCategoryData();
const formCategoryDelete = document.querySelector("#formCategoryDelete");
console.log(formCategoryDelete);
formCategoryDelete.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(formCategoryDelete);
    let id = formData.get("Id");
    console.log(id);
    try {
        await categoryRepo.delete(formData.get("Id"));
        console.log(id);
        window.location.replace("/category/index/");
    }
    catch (error) {
        console.log("Error processinggggg" + error);
    }
});

// method to populate the category data from the database
async function populateCategoryData() {
    try {
        const category = await categoryRepo.read(categoryId);
        console.log(category);
        domCreator.setElementText("#categoryId", category.id);
        domCreator.setElementText("#Name", category.name);
        domCreator.setElementValue("#Id", category.id);
        console.log(category.id);
        domCreator.removeChildren(categoryHeading);
        categoryHeading.appendChild(document.createTextNode("Category"));
    }
    catch (error) {
        console.log(error);
        window.location.replace("/home/index");
    }
}