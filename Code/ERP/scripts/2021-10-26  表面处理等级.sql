BEGIN TRANSACTION;
GO

ALTER TABLE [SubOrderCncItem] ADD [SurfaceLevel] int NOT NULL DEFAULT 0;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'表面处理等级';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'SubOrderCncItem', 'COLUMN', N'SurfaceLevel';
GO

ALTER TABLE [SubOrderCncItem] ADD [SurfaceLevelName] varchar(80) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'表面处理等级名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'SubOrderCncItem', 'COLUMN', N'SurfaceLevelName';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211026031258_AddSurfaceLevel', N'5.0.11');
GO

COMMIT;
GO

