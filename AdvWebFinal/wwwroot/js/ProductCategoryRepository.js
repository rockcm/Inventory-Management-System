////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ProductCategoryRepository.js
// Description: javascript to access the api data for productcategories
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
// NOT IMPLEMENTED
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///
 
"use strict";
export class ProductCategoryRepository {
    #baseAddress;
    constructor(baseAddress) {
        this.#baseAddress = baseAddress;
    }

    // reads all the productcategories
    async readAll() {
        const address = `${this.#baseAddress}/productcategories`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the prodcat data.");
        }
        return await response.json();
    }

    //reads productcategories with the id
    async read(id) {
        const address = `${this.#baseAddress}/productcategories/${id}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the prodcat data.");
        }
        return await response.json();
    }

    //creates productcategory
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

    //udpates a productcategory
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

    //deletes a productcategory
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