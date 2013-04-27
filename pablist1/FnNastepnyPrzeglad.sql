SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION FnNastepnyPrzeglad(@IDP int)
RETURNS date
AS
BEGIN
	declare @d int = (select case count(*)
							 when 0 then 3
							 when 1 then 2
							 else 1 end
					  from Pojazd.Przeglad where ID_Pojazd = @IDP)
	return dateadd("year", @d, (select top 1 DataPrzegladu from Pojazd.Przeglad where ID_Pojazd = @IDP order by DataPrzegladu desc))
END
GO

