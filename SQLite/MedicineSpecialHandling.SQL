CREATE TABLE [MedicineSpecialHandling](
    [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [Narcotic] BOOLEAN NOT NULL,
    [SeperatePouch] BOOLEAN NOT NULL,
    [RobotHandling] BOOLEAN NOT NULL,
    [ExpiryTimeInDays] INT NOT NULL,
    [ExpiryTimeInDaysForPouch] INT NOT NULL,
    [Splitable] BOOLEAN NOT NULL,
    [MaxAmountPerPouch] INT NOT NULL,
    [NeedsCooling] BOOLEAN NOT NULL
)