CREATE TABLE [dbo].[Sms] (
    [id]             INT  IDENTITY (1, 1) NOT NULL,
    [ID_TekstySms]   INT  NOT NULL,
    [ID_Pojazd]      INT  NOT NULL,
    [DataWyslania]   DATE NULL,
    [DataNastepnego] DATE NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC), 
    CONSTRAINT [FK_Sms_To_TekstySms] FOREIGN KEY ([ID_TekstySms]) REFERENCES [TekstySms]([id]), 
    CONSTRAINT [FK_Sms_To_Pojazd] FOREIGN KEY ([ID_Pojazd]) REFERENCES [Pojazd]([id])
);

