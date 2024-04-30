"use strict";

export class DOMCreator {
    createTextTD(text) {
        const td = document.createElement("td");
        td.appendChild(document.createTextNode(text));
        return td;
    }

    createImageTR(src, alt) {
        const tr = document.createElement("tr");
        const td = this.createImageTD(src, alt);
        td.setAttribute("colspan", "4");
        tr.appendChild(td);
        return tr;
    }

    createImageTD(src, alt) {
        const td = document.createElement("td");
        const img = this.createImg(src, alt);

        // Set the width and height of the image to 100 pixels
        img.style.width = "100px";
        img.style.height = "100px";

        td.appendChild(img);
        return td;
    }

    createImg(src, alt) {
        const img = document.createElement("img");
        img.setAttribute("src", src); 
        img.setAttribute("alt", alt);
        img.style.width = "100px";
        img.style.height = "100px";
        return img;
    }

    createTextLink(url, text) {
        const a = document.createElement("a");
        a.setAttribute("href", url);
        a.appendChild(document.createTextNode(text));
        return a;
    }

    removeChildren(element) {
        while (element.firstChild) {
            element.removeChild(element.firstChild);
        }
    }

    setElementText(elementId, text) {
        const element = document.querySelector(elementId);
        element.appendChild(document.createTextNode(text));
    }

    setElementValue(elementId, value) {
        const element = document.querySelector(elementId);
        element.value = value;
    }
}
