////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: ProductRepository.js
// Description: javascript to access the api data for product
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///


"use strict";
export class ProductRepository {
    #baseAddress;
    constructor(baseAddress) {
        this.#baseAddress = baseAddress;
    }

    // reads all the products 
    async readAll() {
        const address = `${this.#baseAddress}/products`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the product data.");
        }
        return await response.json();
    }

    //reads product with the id
    async read(id) {
        const address = `${this.#baseAddress}/products/${id}`;
        const response = await fetch(address);
        if (!response.ok) {
            throw new Error("There was an HTTP error getting the product data.");
        }
        return await response.json();
    }

    //creates product
    async create(formData) {
        const address = `${this.#baseAddress}/createproduct`;
        const response = await fetch(address, {
            method: "post",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error creating the product data.");
        }
        return await response.json();
    }

    //udpates a product
    async update(formData) {
        const address = `${this.#baseAddress}/update`;
        const response = await fetch(address, {
            method: "put",
            body: formData
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error updating the product data.");
        }
        return await response.text();
    }
    //deletes a product
    async delete(id) {
        const address = `${this.#baseAddress}/product/delete/${id}`;
        const response = await fetch(address, {
            method: "delete"
        });
        if (!response.ok) {
            throw new Error("There was an HTTP error deleting the product data.");
        }
        return await response.text();
    }


}