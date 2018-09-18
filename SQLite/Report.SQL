CREATE TABLE [Report](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [LastTimeExecuted] DATETIME NOT NULL,
    [Description] NVARCHAR NULL,
    [Name] NVARCHAR NOT NULL,
    [Status] INT NOT NULL,
    [AmountOfNewReports] INT NOT NULL,
    [Type] INT NOT NULL,
    [IsUserTemplate] BOOLEAN NOT NULL,
    [XtraReportObject] BLOB NULL,
    [UsedDatabase] INT NOT NULL
)