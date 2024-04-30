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