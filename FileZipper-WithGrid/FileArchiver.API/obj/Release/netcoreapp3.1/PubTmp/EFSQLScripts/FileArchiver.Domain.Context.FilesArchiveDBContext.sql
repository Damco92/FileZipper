IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211017124953_Init')
BEGIN
    CREATE TABLE [DocumentTypes] (
        [Id] int NOT NULL IDENTITY,
        [DocumentName] varchar(50) NULL,
        [FileNameMask] nvarchar(max) NULL,
        CONSTRAINT [PK_DocumentTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211017124953_Init')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Username] varchar(100) NOT NULL,
        [Name] varchar(200) NOT NULL,
        [ZipPassword] varchar(128) NOT NULL,
        [IsAdmin] bit NOT NULL DEFAULT CAST(1 AS bit),
        [IsActive] bit NOT NULL,
        [Created] SMALLDATETIME NOT NULL,
        [Creator] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211017124953_Init')
BEGIN
    CREATE TABLE [Files] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [DocumentTypeId] int NOT NULL,
        [IsDownloaded] bit NULL,
        [FilePath] nvarchar(200) NULL,
        [Data] varbinary(8000) NULL,
        [Created] SMALLDATETIME NOT NULL,
        [Creator] int NOT NULL,
        CONSTRAINT [PK_Files] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_DocumentTypeId] FOREIGN KEY ([DocumentTypeId]) REFERENCES [DocumentTypes] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211017124953_Init')
BEGIN
    CREATE INDEX [IX_Files_DocumentTypeId] ON [Files] ([DocumentTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211017124953_Init')
BEGIN
    CREATE INDEX [IX_Files_UserId] ON [Files] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211017124953_Init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211017124953_Init', N'3.1.0');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211019144151_Add_Column')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Files]') AND [c].[name] = N'FilePath');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Files] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Files] DROP COLUMN [FilePath];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211019144151_Add_Column')
BEGIN
    ALTER TABLE [Files] ADD [FileName] nvarchar(200) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211019144151_Add_Column')
BEGIN
    ALTER TABLE [DocumentTypes] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211019144151_Add_Column')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211019144151_Add_Column', N'3.1.0');
END;

GO

