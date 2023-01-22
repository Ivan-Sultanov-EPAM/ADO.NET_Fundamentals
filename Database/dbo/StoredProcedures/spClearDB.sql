﻿CREATE PROCEDURE [dbo].[spClearDB]
AS
 BEGIN
	TRUNCATE TABLE orders

	ALTER TABLE orders DROP CONSTRAINT FK_Orders_Products

	TRUNCATE TABLE products

	ALTER TABLE orders
	ADD CONSTRAINT FK_Orders_Products
	FOREIGN KEY (product_id) REFERENCES Products(Id);
 END
GO
