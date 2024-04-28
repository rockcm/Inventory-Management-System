"use strict"
import { CategoryRepository } from "./CategoryRepository.js";


const categoryRepo = new CategoryRepository("https://localhost:7095/api");

const categoryCreateForm = document.querySelector("#formCreateCategory");
categoryCreateForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(categoryCreateForm);
    const result = await categoryRepo.create(formData);
    console.log(result);
    window.location.replace("/category/index");
});