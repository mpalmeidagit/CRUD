USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Recuperar serviço pelo ID
-- =============================================
CREATE PROCEDURE STP_RECUPERAR_SERVICO_PELO_ID(
	@prmId int
)
AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_servico
	WHERE 
		id = @prmId and ativo = 1
END
GO
