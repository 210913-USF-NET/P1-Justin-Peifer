
# Beelicious Store App
## Overview
There are a diverse range of honeys from all over the world, yet grocery stores only end up selling the cheapest, mixed brands instead. *Beelicious* sells the best honey you can find. Search no further, you can order from our 3 stores and have quality honey at your doorstep in no time!

Azure link: https://beelicious.azurewebsites.net/

## Tech Stack:
* C#
* EF Core
* ASP.NET MVC
* PostgreSQL DB
* Github Actions
* Azure 
* Xunit
* Serilog

## Functionality:
* Add a new customer
* Search customers by name
* Display details of an order
* Place orders to store locations for customers
* View order history of customer
* View order history of location
* View location inventory
* The customer should be able to purchase multiple products
* Order histories have the option to be sorted by date  or cost by clicking on the table.
* The manager can edit the inventory of each store

## User Stories
* As a customer, I can use my username and password to sign in.
* As a customer, I can check if I already have an existing account when signing up.
* As a customer, I can select a store and add location to my profile.
* As a customer, I can view a list of available products. 
* As a customer, I am able to order from multiple stores using the same shopping cart.
* As a customer, I can select multiple products and add quantity to a shopping cart. 
* As a customer, I can get notified if I put quantities that are more than in stock.
* As a customer, I can view a shopping cart and go back to shopping.
* As a customer, I can view a shopping cart if I have logged out without checking out.
* As a customer, I can edit or delete items in a shopping cart.
* As a customer, I can get confirmation if my order is placed successfully. 
* As a customer, I can view my order histories with details. 
* As a customer, I can change my user inforamtion.
* As a manager, I can choose a location.
* As a manager, I can edit an existing location.
* As a manager, I can add a new customer.
* As a manager, I can edit an existing customer and make them admin as well.
* As a manager, I can add a new product.
* As a manager, I can edit an existing product.
* As a manager, I can view and select products that have no inventory yet. 
* As a manager, I can add inventory of a specific product to a specific location.
* As a manager, I can view inventory stock by location.
* As a manager, I can update inventory to a specific product.

## Additional Features
* Exception Handling
* Input validation
* Unit tests
* Use Moq and Sqlite for testing
* Data are persisted
* Use PostgreSQL DB to store data
* Use code first approach to establish a connection to the DB
* WebApp is deployed using Azure App Services
* A CI/CD pipeline is established use Azure Pipelines
* Use ASP.NET MVC for the UI
* DB structure is 3NF
* Have an ER Diagram

## ERD
* https://lucid.app/lucidchart/e36079de-b1cc-4a0e-b69a-4a7fe40c7c14/edit?viewport_loc=45%2C-53%2C1707%2C825%2C0_0&invitationId=inv_07aff894-b60a-43fb-8677-b46a5c351bfc
