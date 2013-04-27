CREATE TABLE [dbo].[Przeglad] (
    [id]               INT        IDENTITY (1, 1) NOT NULL,
    [ID_Pojazd]        INT        NOT NULL,
    [DataPlanowana]    DATE       NOT NULL,
    [DataNastepnego]   DATE       NULL,
    [Zatwierdzony]     BINARY (1) NULL,
    [ID_Przyjmujacego] INT        NOT NULL,
    [ID_Wykonujacego]  INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC), 
    CONSTRAINT [FK_Przeglad_To_Pojazd] FOREIGN KEY ([ID_Pojazd]) REFERENCES [Pojazd]([id]), 
    CONSTRAINT [FK_Przeglad_To_Pracownik1] FOREIGN KEY ([ID_Przyjmujacego]) REFERENCES [Pracownik]([id]), 
    CONSTRAINT [FK_Przeglad_To_Pracownik2] FOREIGN KEY ([ID_Wykonujacego]) REFERENCES [Pracownik]([id])
);

