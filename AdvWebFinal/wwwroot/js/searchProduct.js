// searchProducts.js
"use strict";

import { ProductRepository } from "./ProductRepository.js";

const productRepo = new ProductRepository("https://localhost:7095/api");

const searchForm = document.getElementById("searchForm");
const searchQueryInput = document.getElementById("searchQuery");
const searchResultsDiv = document.getElementById("searchResults");

searchForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const searchQuery = searchQueryInput.value.trim();
    if (searchQuery === "") {
        alert("Please enter a search query.");
        return;
    }
    try {
        const searchResults = await productRepo.search(searchQuery);
        displaySearchResults(searchResults);
    } catch (error) {
        console.error("Error searching products:", error);
    }
});

function displaySearchResults(products) {
    if (products.length === 0) {
        searchResultsDiv.innerHTML = "<p>No products found.</p>";
        return;
    }
    const resultsHTML = products.map(product => `
        <div>
            <h3>${product.name}</h3>
            <p>Description: ${product.description}</p>
            <p>Price: $${product.sellPrice}</p>
            <hr>
        </div>
    `).join("");
    searchResultsDiv.innerHTML = resultsHTML;
}