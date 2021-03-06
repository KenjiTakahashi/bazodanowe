USE [master]
GO
/****** Object:  Database [test]    Script Date: 2013-03-21 17:34:31 ******/
CREATE DATABASE [test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\test.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\test_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [test] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [test] SET ARITHABORT OFF 
GO
ALTER DATABASE [test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [test] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [test] SET RECOVERY FULL 
GO
ALTER DATABASE [test] SET  MULTI_USER 
GO
ALTER DATABASE [test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [test] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'test', N'ON'
GO
USE [test]
GO
/****** Object:  Schema [Pojazd]    Script Date: 2013-03-21 17:34:32 ******/
CREATE SCHEMA [Pojazd]
GO
/****** Object:  Schema [Powiadomienie]    Script Date: 2013-03-21 17:34:32 ******/
CREATE SCHEMA [Powiadomienie]
GO
/****** Object:  Schema [Uzytkownik]    Script Date: 2013-03-21 17:34:32 ******/
CREATE SCHEMA [Uzytkownik]
GO
/****** Object:  UserDefinedDataType [Uzytkownik].[pesel]    Script Date: 2013-03-21 17:34:32 ******/
CREATE TYPE [Uzytkownik].[pesel] FROM [nchar](11) NOT NULL
GO
/****** Object:  UserDefinedDataType [Uzytkownik].[phone]    Script Date: 2013-03-21 17:34:32 ******/
CREATE TYPE [Uzytkownik].[phone] FROM [varchar](25) NULL
GO
/****** Object:  StoredProcedure [dbo].[SpPrzegladyOdDo]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpPrzegladyOdDo]
	@start date,
	@end date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select * from Pojazd.Przeglad where (DataPlanowana >= @start and DataPlanowana <= @end)
END

GO
/****** Object:  StoredProcedure [dbo].[SpZarejestrujPrzeglad]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpZarejestrujPrzeglad]
	@IDP int,
	@Success bit OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @Date date = (select top 1 DataNastepnego
						  from Pojazd.Przeglad where ID_Pojazd = @IDP
						  order by DataNastepnego desc)
	if (select count(*) from dbo.FnPracujacy(@Date)) = 0
	begin
		set @Success = 0
	end
	else
	begin
		insert into Pojazd.Przeglad (
									 ID_Pojazd,
									 DataPlanowana,
									 DataPrzegladu,
									 DataNastepnego,
									 ID_Przyjmujacego,
									 ID_Zatwierdzajacego
									) values (
									 @IDP,
									 NULL,
									 @Date,
									 NULL,
									 (select top 1 ID from dbo.FnPracujacy(@Date)),
									 NULL
									)
		update Pojazd.Przeglad
		set DataNastepnego = dbo.FnNastepnyPrzeglad(@IDP)
		where ID_Pojazd = (select top 1 ID from Pojazd.Przeglad where ID_Pojazd = @IDP order by newid())
		set @Success = 1
	end
END

GO
/****** Object:  UserDefinedFunction [dbo].[FnNastepnyPrzeglad]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[FnNastepnyPrzeglad](@IDP int)
RETURNS date
AS
BEGIN
	declare @d int
	set @d = (select case count(*)
					 when 0 then 3
					 when 1 then 2
					 else 1 end
			  from Pojazd.Przeglad where ID_Pojazd = @IDP)
	return dateadd("year", @d, (select top 1 DataPrzegladu from Pojazd.Przeglad where ID_Pojazd = @IDP order by DataPrzegladu desc))
END

GO
/****** Object:  Table [Pojazd].[Pojazd]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Pojazd].[Pojazd](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Klient] [int] NOT NULL,
	[NrRej] [varchar](8) NOT NULL,
	[DataPierwszejRejestracji] [date] NOT NULL,
 CONSTRAINT [PK_Pojazd] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Pojazd].[Przeglad]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Pojazd].[Przeglad](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Pojazd] [int] NOT NULL,
	[DataPlanowana] [date] NULL,
	[DataPrzegladu] [date] NULL,
	[DataNastepnego] [date] NULL,
	[ID_Przyjmujacego] [int] NOT NULL,
	[ID_Zatwierdzajacego] [int] NULL,
 CONSTRAINT [PK_Przeglad] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Powiadomienie].[Sms]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Powiadomienie].[Sms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_SlownikPowiadomien] [int] NOT NULL,
	[ID_Klienta] [int] NOT NULL,
	[DataWyslania] [date] NULL,
	[DataNastepnego] [date] NULL,
 CONSTRAINT [PK_Sms_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Powiadomienie].[TekstySms]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Powiadomienie].[TekstySms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tresc] [ntext] NOT NULL,
 CONSTRAINT [PK_TekstySms_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Uzytkownik].[Klient]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [Uzytkownik].[Klient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PESEL] [Uzytkownik].[pesel] NOT NULL,
	[Nazwisko] [nvarchar](max) NOT NULL,
	[DataRejestracji] [date] NOT NULL,
	[Telefon] [Uzytkownik].[phone] NULL,
 CONSTRAINT [PK_Klient_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Uzytkownik].[Pracownik]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Uzytkownik].[Pracownik](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nazwisko] [nvarchar](max) NOT NULL,
	[Rola] [nvarchar](max) NOT NULL,
	[Login] [nvarchar](max) NOT NULL,
	[Haslo] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Pracownik] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Uzytkownik].[Urlop]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Uzytkownik].[Urlop](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Pracownik] [int] NOT NULL,
	[Data] [date] NOT NULL,
 CONSTRAINT [PK_Urlop] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  UserDefinedFunction [dbo].[FnPracujacy]    Script Date: 2013-03-21 17:34:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FnPracujacy](@d date)
RETURNS TABLE 
AS
RETURN 
(
	SELECT Uzytkownik.Pracownik.*
	FROM Uzytkownik.Pracownik INNER JOIN
		 Uzytkownik.Urlop ON Uzytkownik.Pracownik.ID = Uzytkownik.Urlop.ID_Pracownik
	WHERE Uzytkownik.Urlop.Data != @d
)

GO
ALTER TABLE [Pojazd].[Pojazd]  WITH CHECK ADD  CONSTRAINT [FK_Pojazd_Klient] FOREIGN KEY([ID_Klient])
REFERENCES [Uzytkownik].[Klient] ([ID])
GO
ALTER TABLE [Pojazd].[Pojazd] CHECK CONSTRAINT [FK_Pojazd_Klient]
GO
ALTER TABLE [Pojazd].[Przeglad]  WITH CHECK ADD  CONSTRAINT [FK_Przeglad_Pojazd] FOREIGN KEY([ID_Pojazd])
REFERENCES [Pojazd].[Pojazd] ([ID])
GO
ALTER TABLE [Pojazd].[Przeglad] CHECK CONSTRAINT [FK_Przeglad_Pojazd]
GO
ALTER TABLE [Pojazd].[Przeglad]  WITH CHECK ADD  CONSTRAINT [FK_Przeglad_Pracownik] FOREIGN KEY([ID_Przyjmujacego])
REFERENCES [Uzytkownik].[Pracownik] ([ID])
GO
ALTER TABLE [Pojazd].[Przeglad] CHECK CONSTRAINT [FK_Przeglad_Pracownik]
GO
ALTER TABLE [Pojazd].[Przeglad]  WITH CHECK ADD  CONSTRAINT [FK_Przeglad_Pracownik1] FOREIGN KEY([ID_Zatwierdzajacego])
REFERENCES [Uzytkownik].[Pracownik] ([ID])
GO
ALTER TABLE [Pojazd].[Przeglad] CHECK CONSTRAINT [FK_Przeglad_Pracownik1]
GO
ALTER TABLE [Powiadomienie].[Sms]  WITH CHECK ADD  CONSTRAINT [FK_Sms_Klient] FOREIGN KEY([ID_Klienta])
REFERENCES [Uzytkownik].[Klient] ([ID])
GO
ALTER TABLE [Powiadomienie].[Sms] CHECK CONSTRAINT [FK_Sms_Klient]
GO
ALTER TABLE [Powiadomienie].[Sms]  WITH CHECK ADD  CONSTRAINT [FK_Sms_TekstySms] FOREIGN KEY([ID_SlownikPowiadomien])
REFERENCES [Powiadomienie].[TekstySms] ([ID])
GO
ALTER TABLE [Powiadomienie].[Sms] CHECK CONSTRAINT [FK_Sms_TekstySms]
GO
ALTER TABLE [Uzytkownik].[Urlop]  WITH CHECK ADD  CONSTRAINT [FK_Urlop_Pracownik] FOREIGN KEY([ID_Pracownik])
REFERENCES [Uzytkownik].[Pracownik] ([ID])
GO
ALTER TABLE [Uzytkownik].[Urlop] CHECK CONSTRAINT [FK_Urlop_Pracownik]
GO
USE [master]
GO
ALTER DATABASE [test] SET  READ_WRITE 
GO
