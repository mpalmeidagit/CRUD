USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Remover serviço
-- =============================================
CREATE PROCEDURE STP_REMOVER_SERVICO(
	@prmId int
)
AS
BEGIN
		UPDATE tab_servico SET ativo = 0	WHERE id = @prmId
END
GO
