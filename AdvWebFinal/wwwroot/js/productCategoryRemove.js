"use strict";

import { ProductCategoryRepository } from "./ProductCategoryRepository.js";

const productCategoryRepo = new ProductCategoryRepository("https://localhost:7095/api");

const formProductCategoryDelete = document.querySelector("#formProductCategoryDelete");

formProductCategoryDelete.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(formProductCategoryDelete);
    const productCategoryId = formData.get("Id");

    try {
        await productCategoryRepo.delete(productCategoryId);
        window.location.replace("/productcategory/index");
    } catch (error) {
        console.error("Error deleting product category:", error);
    }
});