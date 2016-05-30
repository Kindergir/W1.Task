CREATE TABLE Products
(
ProductID int NOT NULL PRIMARY KEY IDENTITY,
Name nvarchar(100) NOT NULL,
[Description] nvarchar(500) NOT NULL,
Category nvarchar(50) NOT NULL,
Price decimal(16,2) NOT NULL
)

alter table Products add ImageData varbinary(max);
alter table Products add ImageMimeType varchar(50);

insert into Products (Name, Description, Category, Price)
values ('Alenka', 'Milk chocolate', 'Sweets', 69.90);
insert into Products (Name, [Description], Category, Price)
values ('Milka', 'Milk chocolate', 'Sweets', 69.90);
insert into Products (Name, [Description], Category, Price)
values ('Alpen Gold', 'Milk chocolate', 'Sweets', 48.90);
insert into Products (Name, [Description], Category, Price)
values ('Fazer', 'Milk chocolate', 'Sweets', 98.70);
insert into Products (Name, [Description], Category, Price)
values ('Ritter sport', 'Milk chocolate', 'Sweets', 120.90);
insert into Products (Name, [Description], Category, Price)
values ('Ritter sport', 'Milk chocolate chocolate with whole nuts', 'Sweets', 120.90);
insert into Products (Name, [Description], Category, Price)
values ('Dove', 'Milk chocolate', 'Sweets', 85.00);

update Products set [Category] = 'Chocolate';
update Products set ImageMimeType = NULL, ImageData = NULL;
update Products set [Description] = 'Milk chocolate' where ProductID != 6;
update Products set [Description] = 'Milk chocolate';

insert into Products (Name, Description, Category, Price)
values ('Nougat', 'Ñandy filled with vanilla nougat, 300 g', 'Candies', 120.0);
insert into Products (Name, Description, Category, Price)
values ('Step Platinum', 'Candy stuffed with nougat, nuts and caramel, 300 g', 'Candies', 150.0);
insert into Products (Name, Description, Category, Price)
values ('Step Super', 'Ñandy filled with caramel, nuts and raisins, 300 g', 'Candies', 150.0);
insert into Products (Name, Description, Category, Price)
values ('Nougat', 'Ñandy filled with strawberry nougat, 300 g', 'Candies', 120.0);
insert into Products (Name, Description, Category, Price)
values ('Step', 'Ñandy filled with caramel and nuts, 300 g', 'Candies', 150.0);
insert into Products (Name, Description, Category, Price)
values ('Lomtishka', 'Very tasty soft candy, 300 g', 'Candies', 160.0);
insert into Products (Name, Description, Category, Price)
values ('Kitkat', 'Wafer in milk chocolate', 'Candies', 130.0);
insert into Products (Name, Description, Category, Price)
values ('Korovka', 'Soft toffee', 'Candies', 120.0);
insert into Products (Name, Description, Category, Price)
values ('Kinder Surprise', 'Chocolate egg with a toy', 'Chocolate eggs', 80.0);
insert into Products (Name, Description, Category, Price)
values ('Kinder Surprise: Maxi', 'Chocolate egg with a toy', 'Chocolate eggs', 120.0);