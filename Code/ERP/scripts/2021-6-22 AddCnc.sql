BEGIN TRANSACTION;
GO

CREATE TABLE [Cnc_Bom] (
    [Id] uniqueidentifier NOT NULL,
    [OrderNo] nvarchar(30) NOT NULL,
    [ProName] nvarchar(50) NULL,
    [Picture] varchar(200) NULL,
    [FileName] nvarchar(100) NULL,
    [FilePath] varchar(200) NULL,
    [Material] int NULL,
    [Qty] int NOT NULL,
    [Size] nvarchar(30) NULL,
    [Surface] int NULL,
    [Remark] varchar(500) NULL,
    [ExtraProperties] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(40) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    CONSTRAINT [PK_Cnc_Bom] PRIMARY KEY ([Id])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'CNC操作流程表';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom';
SET @description = N'订单编号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'OrderNo';
SET @description = N'产品名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'ProName';
SET @description = N'产品图片';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'Picture';
SET @description = N'产品文件名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'FileName';
SET @description = N'产品文件路径';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'FilePath';
SET @description = N'产品材质(材料)';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'Material';
SET @description = N'数量';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'Qty';
SET @description = N'尺寸';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'Size';
SET @description = N'表面处理';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'Surface';
SET @description = N'备注';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Bom', 'COLUMN', N'Remark';
GO

CREATE TABLE [Cnc_Flow] (
    [Id] uniqueidentifier NOT NULL,
    [OrderNo] nvarchar(30) NOT NULL,
    [Type] int NOT NULL,
    [Remark] varchar(500) NULL,
    [Note] varchar(500) NULL,
    [ExtraProperties] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(40) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    CONSTRAINT [PK_Cnc_Flow] PRIMARY KEY ([Id])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'CNC操作流程表';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Flow';
SET @description = N'订单编号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Flow', 'COLUMN', N'OrderNo';
SET @description = N'流程类型';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Flow', 'COLUMN', N'Type';
SET @description = N'备注';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Flow', 'COLUMN', N'Remark';
SET @description = N'操作记录';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Flow', 'COLUMN', N'Note';
GO

CREATE TABLE [Cnc_Order] (
    [Id] uniqueidentifier NOT NULL,
    [OrderNo] varchar(32) NOT NULL,
    [MainOrderNo] varchar(32) NOT NULL,
    [Status] int NOT NULL,
    [Remark] nvarchar(200) NULL,
    [CustomerRemark] nvarchar(200) NULL,
    [TenantId] uniqueidentifier NULL,
    [ExtraProperties] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(40) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [DeleterId] uniqueidentifier NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Cnc_Order] PRIMARY KEY ([Id])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'CNC订单表';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Order';
SET @description = N'订单号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Order', 'COLUMN', N'OrderNo';
SET @description = N'主订单号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Order', 'COLUMN', N'MainOrderNo';
SET @description = N'订单状态';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Order', 'COLUMN', N'Status';
SET @description = N'特殊备注';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Order', 'COLUMN', N'Remark';
SET @description = N'客服备注';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Cnc_Order', 'COLUMN', N'CustomerRemark';
GO

CREATE INDEX [Index_CncFlow_OrderNo] ON [Cnc_Bom] ([OrderNo]);
GO

CREATE INDEX [Index_CncFlow_OrderNo1] ON [Cnc_Flow] ([OrderNo]);
GO

CREATE INDEX [Index_CncOrder_OrderNO] ON [Cnc_Order] ([OrderNo]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210622061217_AddCnc', N'5.0.7');
GO

COMMIT;
GO

