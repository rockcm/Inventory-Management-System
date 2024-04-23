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