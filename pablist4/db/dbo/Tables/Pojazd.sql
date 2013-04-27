CREATE TABLE [dbo].[Pojazd] (
    [id]                       INT          IDENTITY (1, 1) NOT NULL,
    [id_klient]                INT          NOT NULL,
    [NrRej]                    VARCHAR (50) NOT NULL,
    [DataPierwszejRejestracji] DATE         NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC), 
    CONSTRAINT [FK_Pojazd_To_Klient] FOREIGN KEY ([id_klient]) REFERENCES [Klient]([id]) 
);

