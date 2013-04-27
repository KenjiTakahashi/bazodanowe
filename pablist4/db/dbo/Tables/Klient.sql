CREATE TABLE [dbo].[Klient] (
    [id]              INT                  IDENTITY (1, 1) NOT NULL,
    [pesel]           [dbo].[pesel]        NOT NULL,
    [imie]            VARCHAR (50)         NOT NULL,
    [nazwisko]        VARCHAR (50)         NOT NULL,
    [email]           VARCHAR (50)         NOT NULL,
    [datarejestracji] DATETIME             NOT NULL,
    [telefon]         [dbo].[phone_number] NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC) 
);

