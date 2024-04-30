"use strict";
import { ProductRepository } from "./ProductRepository.js";
import { DOMCreator } from "./domCreator.js";

const productRepo = new ProductRepository("https://localhost:7095/api");
const domCreator = new DOMCreator();
const urlSections = window.location.href.split("/");
const productId = urlSections[5];

try {
    const product = await productRepo.read(productId);
    console.log(product);

    // Set other product details using setElementText function
    domCreator.setElementText("#productId", product.id);
    domCreator.setElementText("#Name", product.name);
    domCreator.setElementText("#Description", product.description);
    domCreator.setElementText("#PurchasePrice", product.purchasePrice);
    domCreator.setElementText("#SellPrice", product.sellPrice);
    domCreator.setElementText("#Stock", product.stock);

    const img = domCreator.createImg(product.image, product.name);
    img.style.width = "100px"; 

    // Append the image element to the Image container
    const imageContainer = document.querySelector("#Image");
    domCreator.removeChildren(imageContainer); // Remove any existing content
    imageContainer.appendChild(img);
}
catch (error) {
    console.log(error);
    window.location.replace("/index");
}