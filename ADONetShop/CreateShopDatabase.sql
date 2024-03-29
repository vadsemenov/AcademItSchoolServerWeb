CREATE DATABASE Shop;
GO

USE Shop;
GO

CREATE TABLE Category
(
	Id INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	Name NVARCHAR(255) NOT NULL CHECK(Name <> N'')
);
GO

CREATE TABLE Product
(
	Id INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	Name NVARCHAR(255) NOT NULL CHECK(Name <> N''),
	CategoryId INT NOT NULL,
	FOREIGN KEY(CategoryId) REFERENCES Category(Id)
);
GO

INSERT Category(Name) 
VALUES (N'Food'),
		(N'Toy');
GO

INSERT Product(Name, CategoryId) 
VALUES (N'Car', 2),
		(N'Tomato', 1),
		(N'Onion', 1);
GO