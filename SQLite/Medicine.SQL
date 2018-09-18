CREATE TABLE [Medicine](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [AdditionalIdentifier] NVARCHAR NULL,
    [Name] NVARCHAR NULL,
    [Description] NVARCHAR NULL,
    [Comment] NVARCHAR NULL,
    [IsTrayFillOnly] BOOLEAN NOT NULL,
    [ReleaseStatus] INT NOT NULL,
    [PouchMode] INT NOT NULL,
    [Manufacturer_Id] INTEGER NULL,
    [PillDetail_Id] INTEGER NULL,
    [SpecialHandling_Id] INTEGER NULL,
    FOREIGN KEY ([Manufacturer_Id])
    REFERENCES [Manufacturer](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([PillDetail_Id])
    REFERENCES [MedicinePillDetail](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([SpecialHandling_Id])
    REFERENCES [MedicineSpecialHandling](Id)
    ON DELETE SET NULL
)


