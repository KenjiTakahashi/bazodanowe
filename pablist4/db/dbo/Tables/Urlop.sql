CREATE TABLE [dbo].[Urlop] (
    [id]               INT  IDENTITY (1, 1) NOT NULL,
    [id_pracownik]     INT  NOT NULL,
    [data_rozpoczecia] DATE NOT NULL,
    [data_zakonczenia] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC), 
    CONSTRAINT [FK_Urlop_To_Pracownik] FOREIGN KEY ([id_pracownik]) REFERENCES [Pracownik]([id])
);

