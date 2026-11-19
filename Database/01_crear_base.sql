IF DB_ID(N'InventarioDB') IS NULL
    CREATE DATABASE InventarioDB;
GO

USE InventarioDB;
GO

IF OBJECT_ID(N'dbo.Categorias', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Categorias (
        Id          INT IDENTITY(1,1) NOT NULL,
        Nombre      NVARCHAR(50)  NOT NULL,
        Descripcion NVARCHAR(200) NULL,
        CONSTRAINT PK_Categorias        PRIMARY KEY (Id),
        CONSTRAINT UQ_Categorias_Nombre UNIQUE (Nombre)
    );
END
GO

IF OBJECT_ID(N'dbo.Productos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Productos (
        Id            INT IDENTITY(1,1) NOT NULL,
        Nombre        NVARCHAR(50)  NOT NULL,
        Descripcion   NVARCHAR(200) NULL,
        Precio        DECIMAL(18,2) NOT NULL,
        Stock         INT           NOT NULL,
        FechaRegistro DATETIME2     NOT NULL CONSTRAINT DF_Productos_FechaRegistro DEFAULT (GETUTCDATE()),
        CategoriaId   INT           NOT NULL,
        CONSTRAINT PK_Productos        PRIMARY KEY (Id),
        CONSTRAINT UQ_Productos_Nombre UNIQUE (Nombre),
        CONSTRAINT CK_Productos_Precio CHECK (Precio > 0),
        CONSTRAINT CK_Productos_Stock  CHECK (Stock >= 0),
        CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaId) REFERENCES dbo.Categorias (Id)
    );
    CREATE INDEX IX_Productos_CategoriaId ON dbo.Productos (CategoriaId);
END
GO
