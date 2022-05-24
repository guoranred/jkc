BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'DeliveryDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Injection_Order] DROP COLUMN [DeliveryDate];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InjectionOrder]') AND [c].[name] = N'DeliveryDays');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [InjectionOrder] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Injection_Order] DROP COLUMN [DeliveryDays];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210602071235_RemmoveInjectionOrderDeliveryDaysAndDeliveryDate', N'5.0.6');
GO

COMMIT;
GO

