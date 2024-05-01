"use strict";
export class ProductCategoryRepository {
    #baseAddress;
    constructor(baseAddress) {
        this.#baseAddress = baseAddress;
    }

    async readAll() {
        const address = `${this.#baseAddress}/productcategories`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the prodcat data.");
        }
        return await response.json();
    }

    async read(id) {
        const address = `${this.#baseAddress}/productcategories/${id}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the prodcat data.");
        }
        return await response.json();
    }

    async create(formData) {
        const address = `${this.#baseAddress}/createproductcategory`;
        const response = await fetch(address, {
            method: "post",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error creating the data.");
        }
        return await response.json();
    }

    async update(formData) {
        const address = `${this.#baseAddress}/productcategories/update`;
        const response = await fetch(address, {
            method: "put",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error updating the prodcat data.");
        }
        return await response.text();
    }


    async delete(id) {
        const address = `${this.#baseAddress}/productcategories/delete/${id}`;
        console.log("Test Adress" + address);
        const response = await fetch(address, {
            method: "delete"
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error deeleting the prodcat data.");
        }
        return await response.text();
    }


}