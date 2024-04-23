"use strict"
import { ProductRepository } from "./ProductRepository.js";
import { DOMCreator } from "./domCreator.js";
const productRepo = new ProductRepository("https://localhost:7095/api");
const domCreator = new DOMCreator();



const productTableBody = document.querySelector("#productTableBody");
productTableBody.appendChild(domCreator.createImageTR("./images/ajax-loader.gif",
    "Loading image"));
let products = await productRepo.readAll();
domCreator.removeChildren(productTableBody);
products.forEach((product) => {
    productTableBody.appendChild(createProductTR(product));
});
function createProductTR(product) {
    const tr = document.createElement("tr");
    tr.appendChild(domCreator.createTextTD(product.id));
    tr.appendChild(domCreator.createTextTD(product.name));
    tr.appendChild(domCreator.createTextTD(product.description));
    tr.appendChild(domCreator.createTextTD(product.purchasePrice));
    tr.appendChild(domCreator.createTextTD(product.sellPrice));
    tr.appendChild(domCreator.createTextTD(product.stock));
    tr.appendChild(domCreator.createTextTD(product.image));
    tr.appendChild(createTDWithLinks(product.id))
    return tr;
}
function createTDWithLinks(id) {
    const td = document.createElement("td");
    td.appendChild(domCreator.createTextLink(`/product/update/${id}`, "Edit"));
    td.appendChild(document.createTextNode(" | "));
    td.appendChild(domCreator.createTextLink(`/product/details/${id}`, "Details"));
    td.appendChild(document.createTextNode(" | "));
    td.appendChild(domCreator.createTextLink(`/product/delete/${id}`, "Delete"));
    return td;
}