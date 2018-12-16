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

	DELETE FROM 
		tab_perfil 
	WHERE id = @prmId

END
