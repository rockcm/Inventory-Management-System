Inventory Management System 

Overview

The Inventory Management System is a comprehensive solution designed for managing products, categories, and their associations within an inventory. This project users ASP.Net Core to achieve this and includes a robust API for handling CRUD operations on products, categories, and their relationships.

Features

Products Management: Create, read, update, and delete product entries.

Categories Management: Create, read, update, and delete category entries.

Product-Category Relationships: Manage the association between products and categories.

Technologies Used

ASP.NET Core Web API

C#

Microsoft SQL Server

Bootstrap 

JavaScript/Ajax

Entity Framework Core



API Endpoints:

Products-

GET /api/products: Retrieve all products.

GET /api/products/{id}: Retrieve a product by its ID.

POST /api/createproduct: Create a new product.

PUT /api/update: Update an existing product.

DELETE /api/product/delete/{id}: Delete a product by its ID.

Categories-

GET /api/categories: Retrieve all categories.

GET /api/categories/{id}: Retrieve a category by its ID.

POST /api/createcategory: Create a new category.

PUT /api/updatecategory: Update an existing category.

DELETE /api/category/delete/{id}: Delete a category by its ID.

Product Categories-

GET /api/productcategories: Retrieve all product-category associations.

POST /api/createproductcategory: Create a new product-category association.

DELETE /api/remove: Remove a product from a category (not implemented, done through controller).
