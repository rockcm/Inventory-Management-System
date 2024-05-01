"use strict";
export class CategoryRepository {
    #baseAddress;
    constructor(baseAddress) {
        this.#baseAddress = baseAddress;
    }

    async readAll() {
        const address = `${this.#baseAddress}/categories`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the cat data.");
        }
        return await response.json();
    }

    async read(id) {
        const address = `${this.#baseAddress}/categories/${id}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the cat data.");
        }
        return await response.json();
    }

    async create(formData) {
        const address = `${this.#baseAddress}/createcategory`;
        const response = await fetch(address, {
            method: "post",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error creating the cat data.");
        }
        return await response.json();
    }

    async update(formData) {
        const address = `${this.#baseAddress}/updatecategory`;
        const response = await fetch(address, {
            method: "put",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error updating the cat data.");
        }
        return await response.text();
    }

 
    async delete(id) {
        const address = `${this.#baseAddress}/category/delete/${id}`;
        console.log("Test Adress" + address);
        const response = await fetch(address, {
            method: "delete"
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error deeleting the category data.");
        }
        return await response.text();
    }


}