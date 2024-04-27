"use strict"
import { ProductRepository } from "./ProductRepository.js";
import { DOMCreator } from "./domCreator.js";
const productRepo = new ProductRepository("https://localhost:7095/api");
const domCreator = new DOMCreator();


const productHeading = document.querySelector("#productHeading");
domCreator.removeChildren(productHeading);
productHeading.appendChild(document.createTextNode("Loading..."));
const urlSections = window.location.href.split("/");
const productId = urlSections[5];
await populateProductData();
const formProductDelete = document.querySelector("#formProductDelete");
formProductDelete.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(formProductDelete);
    let id = formData.get("Id");
    console.log(id);
    try {
        await productRepo.delete(formData.get("Id"));
        console.log(id);
        window.location.replace("/product/index/");
    }
    catch (error) {
        console.log("Error processing" + error);
    }
});
async function populateProductData() {
    try {
        const product = await productRepo.read(productId);
        console.log(product);
        domCreator.setElementText("#productId", product.id);
        domCreator.setElementText("#Name", product.name);
        domCreator.setElementText("#Description", product.description);
        domCreator.setElementText("#PurchasePrice", product.purchasePrice);
        domCreator.setElementText("#SellPrice", product.sellPrice);
        domCreator.setElementText("#Stock", product.stock);
        domCreator.setElementText("#Image", product.image);
        domCreator.setElementValue("#Id", product.id);
        console.log(product.id);
        domCreator.removeChildren(productHeading);
        productHeading.appendChild(document.createTextNode("Product"));
    }
    catch (error) {
        console.log(error);
        window.location.replace("/home/index");
    }
}