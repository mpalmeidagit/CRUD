USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 16/12/2018
-- Description:	Recuperar perfil por ID
-- =============================================
CREATE PROCEDURE [STP_RECUPERAR_PERFIL_PELO_ID](
	@prmId int
)
AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_perfil
	WHERE 
		id = @prmId and ativo = 1
END
