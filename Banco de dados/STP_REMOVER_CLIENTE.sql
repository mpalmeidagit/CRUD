USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Remover cliente
-- =============================================
CREATE PROCEDURE [dbo].[STP_REMOVER_CLIENTE](
	@prmId int
) 
AS
BEGIN

	UPDATE tab_cliente SET 
		ativo = 0
	WHERE 
	id = @prmId

END