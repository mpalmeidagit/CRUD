USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Remover servi�o
-- =============================================
CREATE PROCEDURE STP_REMOVER_SERVICO(
	@prmId int
)
AS
BEGIN
		UPDATE tab_servico SET ativo = 0	WHERE id = @prmId
END
GO
