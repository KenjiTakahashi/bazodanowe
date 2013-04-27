SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION FnPracujacy(@d date)
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
