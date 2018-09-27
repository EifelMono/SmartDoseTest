CREATE TABLE [InternalOrderHistory](
    [Id] INTEGER PRIMARY KEY NOT NULL,
    [Identifier] NVARCHAR NULL,
    [MachineNumber] INTEGER NOT NULL,
    [MachineOrderIdentifier] NVARCHAR NULL,
    [Status] INT NOT NULL,
    [Timestamp] DATETIME NOT NULL,
    [ProductionFinishedTimeStamp] DATETIME NOT NULL,
    [ExternalId] NVARCHAR NULL,
    [InternalId] NVARCHAR NULL,
    [HasSkippedMedication] BOOLEAN NOT NULL,
    [Source] INT NOT NULL,
    [ValidationErrors] INT NOT NULL,
    [CustomerHistory_Id] INTEGER NULL,
    [HistoryDate] DATETIME NOT NULL,
    [IsDeleted] BOOL NOT NULL DEFAULT 0,
    FOREIGN KEY ([CustomerHistory_Id])
    REFERENCES [CustomerHistory](Id)
    ON DELETE SET NULL
)