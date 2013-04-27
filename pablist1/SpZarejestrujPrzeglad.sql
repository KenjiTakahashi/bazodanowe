-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SpZarejestrujPrzeglad
	@IDP int,
	@Date date,
	@Success bit OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--declare @Date date = (select top 1 DataNastepnego
	--					  from Pojazd.Przeglad where ID_Pojazd = @IDP
	--					  order by DataNastepnego desc)
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
		where ID_Pojazd = (select top 1 ID from Pojazd.Przeglad where ID_Pojazd = @IDP order by ID)
		set @Success = 1
	end
END
GO
