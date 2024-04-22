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