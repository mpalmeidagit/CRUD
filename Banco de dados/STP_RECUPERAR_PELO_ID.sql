USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 09/12/2018
-- Description:	Recuperar por id
-- =============================================
CREATE PROCEDURE STP_RECUPERAR_PELO_ID(
	@prmId int
)
AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_usuario 
	WHERE 
		id = @prmId and ativo = 1
END
GO
