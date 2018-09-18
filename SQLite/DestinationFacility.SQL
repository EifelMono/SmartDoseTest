CREATE TABLE [DestinationFacility](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [Identifier] NVARCHAR NULL,
    [Status] BOOLEAN NOT NULL,
    [DepartmentName] NVARCHAR NULL,
    [DepartmentCode] NVARCHAR NULL,
    [Description] NVARCHAR NULL,
    [Logo] BLOB NULL,
    [CustomerId] INTEGER NOT NULL,
    [ContactAddress_Id] INTEGER NULL,
    [PouchSchemaDesign_Id] INTEGER NULL,
    [MachineOrder_Id] INTEGER NULL,
    [OriginalId] INTEGER NULL,
    FOREIGN KEY ([ContactAddress_Id])
    REFERENCES [ContactAddress](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([PouchSchemaDesign_Id])
    REFERENCES [PouchSchemaDesign](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([MachineOrder_Id])
    REFERENCES [MachineOrder](Id)
    ON DELETE CASCADE
)