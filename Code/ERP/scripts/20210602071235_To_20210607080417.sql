BEGIN TRANSACTION;
GO

ALTER TABLE [Order] ADD [DeliveryDate] datetime2 NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'交期';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Order', 'COLUMN', N'DeliveryDate';
GO

ALTER TABLE [Order] ADD [DeliveryDays] int NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'交期天数';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Order', 'COLUMN', N'DeliveryDays';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210602072415_AddOrderDeliveryDaysAndDeliveryDate', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [InjectionOrder] DROP CONSTRAINT [PK_InjectionOrder];
GO

EXEC sp_rename N'[InjectionOrder]', N'Injection_Order';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Order';
SET @description = N'注塑订单表';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Order';
GO

ALTER TABLE [Injection_Order] ADD CONSTRAINT [PK_Injection_Order] PRIMARY KEY ([Id]);
GO

CREATE TABLE [Injection_Flow] (
    [Id] uniqueidentifier NOT NULL,
    [OrderNo] nvarchar(30) NOT NULL,
    [Type] int NOT NULL,
    [Remark] varchar(500) NULL,
    [Note] varchar(500) NULL,
    [ExtraProperties] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(40) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    CONSTRAINT [PK_Injection_Flow] PRIMARY KEY ([Id])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'注塑操作流程表';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Flow';
SET @description = N'订单编号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Flow', 'COLUMN', N'OrderNo';
SET @description = N'流程类型';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Flow', 'COLUMN', N'Type';
SET @description = N'备注';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Flow', 'COLUMN', N'Remark';
SET @description = N'操作记录';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Injection_Flow', 'COLUMN', N'Note';
GO

CREATE INDEX [Index_InjectionOrder_OrderNO] ON [Injection_Order] ([OrderNo]);
GO

CREATE INDEX [Index_InjectionFlow_OrderNo] ON [Injection_Flow] ([OrderNo]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210607032934_AddInjectionOrderFlowCreate', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Injection_Order].[ProductName]', N'ProName', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210607080417_UpdateProdectName', N'5.0.6');
GO

COMMIT;
GO

