CREATE PROCEDURE GetTotalCount
	@Category NVARCHAR(50) = NULL
AS
	SELECT COUNT(*) FROM Products
	WHERE @Category IS NULL OR Category = @Category
GO

CREATE Procedure GetCategories
As 
	select distinct Products.Category from Products

create procedure TakeProduct
	@ProductID int = 0
as
	select * from Products where ProductID = @ProductID;

	create procedure TakeProducts
	@Category nvarchar(50) = null,
	@Page int = 1,
	@Count int = 4
as
	select * from Products WHERE Category = @Category OR @Category IS NULL
	ORDER BY ProductID OFFSET (@Page - 1) * @Count ROWS FETCH NEXT @Count ROWS ONLY

create procedure DeleteProduct
	@ProductID int
as 
	delete Products where ProductID = @ProductID;

create procedure InsertProduct
	@Name nvarchar(100),
	@Description nvarchar(500),
	@Category nvarchar(50),
	@Price decimal(16,2),
	@ImageMimeType varchar(50) = null,
	@ImageData varbinary(MAX) = null
as
	insert into Products values (@Name, @Description, @Category, @Price, @ImageData, @ImageMimeType);

create procedure UpdateProduct
	@ProductID int,
	@Name nvarchar(100),
	@Description nvarchar(500),
	@Category nvarchar(50),
	@Price decimal(16,2),
	@ImageMimeType varchar(50) = null,
	@ImageData varbinary(MAX) = null
as
	update Products set
	Name = @Name, 
	[Description] = @Description, 
	Category = @Category,
	Price = @Price, 
	ImageData = @ImageData, 
	ImageMimeType = @ImageMimeType
	where ProductID = @ProductID;