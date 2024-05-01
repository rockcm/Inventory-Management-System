"use strict";

import { ProductCategoryRepository } from "./ProductCategoryRepository.js";

const productCategoryRepo = new ProductCategoryRepository("https://localhost:7095/api");

const productCategoryCreateForm = document.querySelector("#formProductCategoryCreate")

productCategoryCreateForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const formData = new FormData(productCategoryCreateForm);
    const productId = formData.get('productId');
    const catId = formData.get('categoryId');
    console.log(formData);

    try {
        await productCategoryRepo.create(formData);
        console.log("Success: the student's grade was changed");
        window.location.replace(`/product/details/${productId}`); // redirect
    }
    catch (error) {
        console.error('Error:', error);
    }
})