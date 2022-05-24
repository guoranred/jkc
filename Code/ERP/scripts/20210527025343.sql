BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Mold_Order]') AND [c].[name] = N'Qty');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Mold_Order] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Mold_Order] ALTER COLUMN [Qty] int NULL;
ALTER TABLE [Mold_Order] ADD DEFAULT 0 FOR [Qty];
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'InjectionOrder', 'COLUMN', N'Size';
SET @description = N'尺寸';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'InjectionOrder', 'COLUMN', N'Size';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'Picture');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [InjectionOrder] ALTER COLUMN [Picture] varchar(200) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'OrderNo');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [InjectionOrder] ALTER COLUMN [OrderNo] varchar(32) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'MoldOrderNo');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [InjectionOrder] ALTER COLUMN [MoldOrderNo] varchar(32) NOT NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'MainOrderNo');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [InjectionOrder] ALTER COLUMN [MainOrderNo] varchar(32) NOT NULL;
ALTER TABLE [InjectionOrder] ADD DEFAULT '' FOR [MainOrderNo];
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'主订单号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'InjectionOrder', 'COLUMN', N'MainOrderNo';
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'FilePath');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [InjectionOrder] ALTER COLUMN [FilePath] varchar(200) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210527025343_AddInjectionOrderModify', N'5.0.6');
GO

COMMIT;
GO

