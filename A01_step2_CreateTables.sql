-- Create Tables For Retail Store

CREATE TABLE Product (
	ProductID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ItemName VARCHAR(50) NOT NULL,
	Description VARCHAR(500) NULL,
	ImageName VARCHAR(50) NULL,
	Price NUMERIC(18,2) NULL
);

CREATE TABLE Category (
	CategoryID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	CatLabel VARCHAR(50) NOT NULL
);

CREATE TABLE [dbo].[ProductCategory] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [ProductID]  INT NOT NULL,
    [CategoryID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([ProductID] ASC, [CategoryID] ASC),
    CONSTRAINT [FK_dbo.ProductCategory_dbo.Product_ProductID] FOREIGN KEY ([ProductID]) 
          REFERENCES [dbo].[Product] ([ProductID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ProductCategory_dbo.Category_CategoryID] FOREIGN KEY ([CategoryID]) 
          REFERENCES [dbo].[Category] ([CategoryID]) ON DELETE CASCADE
);    