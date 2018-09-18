CREATE TABLE [OrderDetail](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [DestinationFacilityId] INTEGER NOT NULL,
    [ExternalDetailPrintInfo1] NVARCHAR NULL,
    [ExternalDetailPrintInfo2] NVARCHAR NULL,
    [Patient_Id] INTEGER NULL,
    [InternalOrder_Id] INTEGER NOT NULL,
    [Pharmacy_Id] INTEGER NULL,
    FOREIGN KEY ([Patient_Id])
    REFERENCES [Patient](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([InternalOrder_Id])
    REFERENCES [InternalOrder](Id)
    ON DELETE CASCADE,
    FOREIGN KEY ([Pharmacy_Id])
    REFERENCES [Pharmacy](Id)
    ON DELETE SET NULL
)