SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [Description], [ImagePath]) VALUES (1, N'Laptop', CAST(12000.00 AS Decimal(18, 2)), N'School laptop', NULL)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [Description], [ImagePath]) VALUES (2, N'Headphones', CAST(500.00 AS Decimal(18, 2)), N'Wireless headphones', NULL)
INSERT INTO [dbo].[Products] ([Id], [Name], [Price], [Description], [ImagePath]) VALUES (3, N'Mouse', CAST(200.00 AS Decimal(18, 2)), N'USB Mouse', NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
