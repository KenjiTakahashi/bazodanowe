CREATE TABLE [dbo].[Pracownik] (
    [id]       INT          IDENTITY (1, 1) NOT NULL,
    [imie]     VARCHAR (50) NOT NULL,
    [nazwisko] VARCHAR (50) NOT NULL,
    [rola]     INT          NOT NULL,
    [login]    VARCHAR (50) NOT NULL,
    [haslo]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

