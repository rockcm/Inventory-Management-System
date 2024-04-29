"use strict";
export class ProductRepository {
    #baseAddress;
    constructor(baseAddress) {
        this.#baseAddress = baseAddress;
    }

    async readAll() {
        const address = `${this.#baseAddress}/products`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the product data.");
        }
        return await response.json();
    }

    async read(id) {
        const address = `${this.#baseAddress}/products/${id}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the category data.");
        }
        return await response.json();
    }

    async create(formData) {
        const address = `${this.#baseAddress}/createproduct`;
        const response = await fetch(address, {
            method: "post",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error creating the category data.");
        }
        return await response.json();
    }

    async update(formData) {
        const address = `${this.#baseAddress}/update`;
        const response = await fetch(address, {
            method: "put",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error updating the category data.");
        }
        return await response.text();
    }

    async delete(id) {
        const address = `${this.#baseAddress}/product/delete/${id}`;
        const response = await fetch(address, {
            method: "delete"
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error deleting the category data.");
        }
        return await response.text();
    }


}