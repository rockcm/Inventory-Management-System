////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////
//
// Project: Inventory Management System - Final
// File Name: DomCreator.js
// Description: controls the dom 
// Course: CSCI 3110 - Advance Web Development
// Author: Christian Rock
// Created: 04/17/24
// Copyright: Christian Rock, 2024, rockcm@etsu.edu
//
////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////
///


"use strict";



export class DOMCreator {

    // Creates a table data cell with text content
    createTextTD(text) {
        const td = document.createElement("td");
        td.appendChild(document.createTextNode(text));
        return td;
    }

    // Creates a table row with an image inside a table data cell
    createImageTR(src, alt) {
        const tr = document.createElement("tr");
        const td = this.createImageTD(src, alt);
        td.setAttribute("colspan", "4");
        tr.appendChild(td);
        return tr;
    }

    // Creates an image element
    createImageTD(src, alt) {
        const td = document.createElement("td");
        const img = this.createImg(src, alt);

        // Set the width and height of the image to 100 pixels
        img.style.width = "100px";
        img.style.height = "100px";

        td.appendChild(img);
        return td;
    }
    // Creates an image element
    createImg(src, alt) {
        const img = document.createElement("img");
        img.setAttribute("src", src); 
        img.setAttribute("alt", alt);
        img.style.width = "100px";
        img.style.height = "100px";
        return img;
    }
    // Creates a text link
    createTextLink(url, text) {
        const a = document.createElement("a");
        a.setAttribute("href", url);
        a.appendChild(document.createTextNode(text));
        return a;
    }
    // Removes all children from a given element
    removeChildren(element) {
        while (element.firstChild) {
            element.removeChild(element.firstChild);
        }
    }
    // Sets text content for an element
    setElementText(elementId, text) {
        const element = document.querySelector(elementId);
        element.appendChild(document.createTextNode(text));
    }
    // Sets value for an input element
    setElementValue(elementId, value) {
        const element = document.querySelector(elementId);
        element.value = value;
    }
}
