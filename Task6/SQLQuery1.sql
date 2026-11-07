-- Create Database
CREATE DATABASE CafeteriaDB;
GO

USE CafeteriaDB;
GO


-- 1. Categories Table
CREATE TABLE Categories
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500)
);


-- 2. Suppliers Table
CREATE TABLE Suppliers
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20)
);


-- 3. Products Table
CREATE TABLE Products
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL,
    CategoryId INT NOT NULL,
    SupplierId INT NOT NULL,

    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
);


-- 4. PurchaseOrders Table
CREATE TABLE PurchaseOrders
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME2 NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    SupplierId INT NOT NULL,

    FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id)
);


-- 5. PurchaseOrderItems Table
CREATE TABLE PurchaseOrderItems
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    PurchaseOrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,

    FOREIGN KEY (PurchaseOrderId) REFERENCES PurchaseOrders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

USE CafeteriaDB;

SELECT * FROM Categories;
SELECT * FROM Suppliers;
SELECT * FROM Products;
SELECT * FROM PurchaseOrders;
SELECT * FROM PurchaseOrderItems;