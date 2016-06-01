DROP TABLE Products
DROP TABLE Categories

CREATE TABLE Categories
(
	CategoryID TINYINT NOT NULL PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL UNIQUE
)
GO

INSERT INTO Categories
VALUES
('Chocolate eggs'),
('Candies'),
('Chocolate')
GO

CREATE TABLE Products
(
ProductID int NOT NULL PRIMARY KEY IDENTITY,
Name nvarchar(100) NOT NULL,
[Description] nvarchar(500) NOT NULL,
CategoryID tinyint NOT NULL,
Price decimal(16,2) NOT NULL,
ImageData varbinary(max),
ImageMimeType varchar(50)

CONSTRAINT FK_Category_Name FOREIGN KEY (CategoryID)
	REFERENCES Categories (CategoryId)
	ON DELETE CASCADE
	ON UPDATE CASCADE
);
GO

insert into Products (Name, Description, CategoryID, Price)
values ('Lisichka', 'Most delicious wafer', 2, 100.00);
insert into Products (Name, Description, CategoryID, Price)
values ('Alenka', 'Milk chocolate', 3, 69.90);
insert into Products (Name, [Description], CategoryID, Price)
values ('Milka', 'Milk chocolate', 3, 69.90);
insert into Products (Name, [Description], CategoryID, Price)
values ('Alpen Gold', 'Milk chocolate', 3, 48.90);
insert into Products (Name, [Description], CategoryID, Price)
values ('Fazer', 'Milk chocolate', 3, 98.70);
insert into Products (Name, [Description], CategoryID, Price)
values ('Ritter sport', 'Milk chocolate', 3, 120.90);
insert into Products (Name, [Description], CategoryID, Price)
values ('Ritter sport', 'Milk chocolate chocolate with whole nuts', 3, 120.90);
insert into Products (Name, [Description], CategoryID, Price)
values ('Dove', 'Milk chocolate', 3, 85.00);

insert into Products (Name, Description, CategoryID, Price)
values ('Nougat', 'Ñandy filled with vanilla nougat, 300 g', 2, 120.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Step Platinum', 'Candy stuffed with nougat, nuts and caramel, 300 g', 2, 150.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Step Super', 'Ñandy filled with caramel, nuts and raisins, 300 g', 2, 150.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Nougat', 'Ñandy filled with strawberry nougat, 300 g', 2, 120.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Step', 'Ñandy filled with caramel and nuts, 300 g', 2, 150.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Lomtishka', 'Very tasty soft candy, 300 g', 2, 160.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Kitkat', 'Wafer in milk chocolate', 2, 130.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Korovka', 'Soft toffee', 2, 120.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Kinder Surprise', 'Chocolate egg with a toy', 1, 80.0);
insert into Products (Name, Description, CategoryID, Price)
values ('Kinder Surprise: Maxi', 'Chocolate egg with a toy', 1, 120.0);
GO

DROP PROCEDURE GetTotalCount
GO

CREATE PROCEDURE GetTotalCount
	@CategoryName NVARCHAR(50) = NULL
AS
	SELECT COUNT(*) AS 'Count' FROM Products 
	JOIN Categories ON Products.CategoryID = Categories.CategoryId
	WHERE @CategoryName IS NULL OR Categories.CategoryName = @CategoryName
GO

DROP PROCEDURE GetCategories
GO

CREATE PROCEDURE GetCategories
AS
	SELECT * FROM dbo.Categories
GO

DROP PROCEDURE TakeProduct
GO

create procedure TakeProduct
	@ProductID int = 0
as
	select * from Products
	where ProductID = @ProductID;
GO

DROP PROCEDURE TakeProducts
GO

create procedure TakeProducts
	@CategoryName nvarchar(50) = null,
	@Page int = 1,
	@Count int = 4
as
	select * from Products 
	JOIN Categories 
	ON Products.CategoryID = Categories.CategoryId 
	WHERE  Categories.CategoryName = @CategoryName
		OR @CategoryName IS NULL
	ORDER BY ProductID OFFSET (@Page - 1) * @Count ROWS FETCH NEXT @Count ROWS ONLY
GO

DROP PROCEDURE DeleteProduct
GO

create procedure DeleteProduct
	@ProductID int
as 
	delete Products where ProductID = @ProductID;
GO

DROP PROCEDURE InsertProduct
GO

create procedure InsertProduct
	@Name nvarchar(100),
	@Description nvarchar(500),
	@CategoryID int,
	@Price decimal(16,2),
	@ImageMimeType varchar(50) = null,
	@ImageData varbinary(MAX) = null
as
	insert into Products values 
	(@Name, @Description, @CategoryID, @Price, @ImageData, @ImageMimeType);
GO

DROP PROCEDURE UpdateProduct
GO

create procedure UpdateProduct
	@ProductID int,
	@Name nvarchar(100),
	@Description nvarchar(500),
	@CategoryID int,
	@Price decimal(16,2),
	@ImageMimeType varchar(50) = null,
	@ImageData varbinary(MAX) = null
as
	update Products set
	Name = @Name, 
	[Description] = @Description, 
	CategoryID = @CategoryID,
	Price = @Price, 
	ImageData = @ImageData, 
	ImageMimeType = @ImageMimeType
	where ProductID = @ProductID
GO

DROP PROCEDURE AddCategory
GO

CREATE PROCEDURE AddCategory
	@Name nvarchar(50)
AS
	IF EXISTS(SELECT 1 FROM Categories WHERE CategoryName = @Name)
		RETURN
	INSERT INTO Categories
	VALUES (@Name)
GO