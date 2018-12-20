USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 16/12/2018
-- Description:	Remover perfil
-- =============================================
CREATE PROCEDURE [dbo].[STP_REMOVER_PERFIL](
	@prmId int
) 
AS
BEGIN

	
	UPDATE tab_perfil SET ativo = 0	WHERE id = @prmId

END
