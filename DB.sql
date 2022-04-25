IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Job_Grades] (
    [Grade_Level] nvarchar(450) NOT NULL,
    [Lowest_Salary] int NOT NULL,
    [Highest_Salary] int NOT NULL,
    CONSTRAINT [PK_Job_Grades] PRIMARY KEY ([Grade_Level])
);
GO

CREATE TABLE [Jobs] (
    [Job_ID] nvarchar(450) NOT NULL,
    [Job_Title] nvarchar(max) NOT NULL,
    [Min_Salary] int NOT NULL,
    [Max_Salary] int NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Job_ID])
);
GO

CREATE TABLE [Regions] (
    [Region_Id] int NOT NULL IDENTITY,
    [Region_Name] nvarchar(25) NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([Region_Id])
);
GO

CREATE TABLE [Countries] (
    [Country_Id] nvarchar(450) NOT NULL,
    [Country_Name] nvarchar(max) NOT NULL,
    [Region_Id] int NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Country_Id]),
    CONSTRAINT [FK_Countries_Regions_Region_Id] FOREIGN KEY ([Region_Id]) REFERENCES [Regions] ([Region_Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Locations] (
    [Location_Id] int NOT NULL,
    [Street_Address] nvarchar(max) NOT NULL,
    [Postal_Code] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State_Province] nvarchar(max) NOT NULL,
    [CountryId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY ([Location_Id]),
    CONSTRAINT [FK_Locations_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Country_Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Department] (
    [Department_ID] int NOT NULL,
    [Department_Name] nvarchar(max) NOT NULL,
    [Manager_ID] int NOT NULL,
    [Location_ID] int NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([Department_ID]),
    CONSTRAINT [FK_Department_Locations_Location_ID] FOREIGN KEY ([Location_ID]) REFERENCES [Locations] ([Location_Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [employee] (
    [EmployeeID] int NOT NULL IDENTITY,
    [First_Name] nvarchar(max) NULL,
    [Last_Name] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Phone_Number] nvarchar(max) NULL,
    [Job_Id] nvarchar(450) NOT NULL,
    [salary] int NULL,
    [Commission_PCT] int NULL,
    [Manager_ID] int NOT NULL,
    [Department_ID] int NOT NULL,
    CONSTRAINT [PK_employee] PRIMARY KEY ([EmployeeID]),
    CONSTRAINT [FK_employee_Department_Department_ID] FOREIGN KEY ([Department_ID]) REFERENCES [Department] ([Department_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_employee_Jobs_Job_Id] FOREIGN KEY ([Job_Id]) REFERENCES [Jobs] ([Job_ID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Job_History] (
    [Employee_ID] int NOT NULL,
    [Start_Date] datetime2 NOT NULL,
    [End_Date] datetime2 NOT NULL,
    [Job_Id] nvarchar(450) NOT NULL,
    [Department_Id] int NOT NULL,
    CONSTRAINT [PK_Job_History] PRIMARY KEY ([Employee_ID], [Start_Date]),
    CONSTRAINT [FK_Job_History_Department_Department_Id] FOREIGN KEY ([Department_Id]) REFERENCES [Department] ([Department_ID]),
    CONSTRAINT [FK_Job_History_employee_Employee_ID] FOREIGN KEY ([Employee_ID]) REFERENCES [employee] ([EmployeeID]),
    CONSTRAINT [FK_Job_History_Jobs_Job_Id] FOREIGN KEY ([Job_Id]) REFERENCES [Jobs] ([Job_ID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Countries_Region_Id] ON [Countries] ([Region_Id]);
GO

CREATE INDEX [IX_Department_Location_ID] ON [Department] ([Location_ID]);
GO

CREATE INDEX [IX_employee_Department_ID] ON [employee] ([Department_ID]);
GO

CREATE INDEX [IX_employee_Job_Id] ON [employee] ([Job_Id]);
GO

CREATE INDEX [IX_Job_History_Department_Id] ON [Job_History] ([Department_Id]);
GO

CREATE INDEX [IX_Job_History_Job_Id] ON [Job_History] ([Job_Id]);
GO

CREATE INDEX [IX_Locations_CountryId] ON [Locations] ([CountryId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220214114057_A', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_Countries_CountryId];
GO

EXEC sp_rename N'[Locations].[CountryId]', N'Country_Id', N'COLUMN';
GO

EXEC sp_rename N'[Locations].[IX_Locations_CountryId]', N'IX_Locations_Country_Id', N'INDEX';
GO

ALTER TABLE [Locations] ADD CONSTRAINT [FK_Locations_Countries_Country_Id] FOREIGN KEY ([Country_Id]) REFERENCES [Countries] ([Country_Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220214120005_A1', N'6.0.0');
GO

COMMIT;
GO
