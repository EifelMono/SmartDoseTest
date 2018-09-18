CREATE TABLE [Customer](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [PouchSchemeId] INTEGER NOT NULL,
    [Name] NVARCHAR NULL,
    [Identifier] NVARCHAR NULL,
    [CustomerId] INTEGER NOT NULL,
    [Fax] NVARCHAR NULL,
    [Website] NVARCHAR NULL,
    [Description] NVARCHAR NULL,
    [ContactAddress_Id] INTEGER NULL,
    [ContactPerson_Id] INTEGER NULL,
    [MachineOrder_Id] INTEGER NULL,
    [OriginalId] INTEGER NULL,
    FOREIGN KEY ([ContactAddress_Id])
    REFERENCES [ContactAddress](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([ContactPerson_Id])
    REFERENCES [ContactPerson](Id)
    ON DELETE SET NULL,
    FOREIGN KEY ([MachineOrder_Id])
    REFERENCES [MachineOrder](Id)
    ON DELETE CASCADE
)