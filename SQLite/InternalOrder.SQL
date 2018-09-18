CREATE TABLE [InternalOrder](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
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
    [Customer_Id] INTEGER NULL,
    FOREIGN KEY ([Customer_Id])
    REFERENCES [Customer](Id)
    ON DELETE SET NULL
)