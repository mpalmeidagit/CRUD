USE[CRUD]
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE STP_REMOVER_RECIBO(
	@prmId int
) 
AS
BEGIN

	UPDATE tab_recibo SET 
		ativo = 0
	WHERE 
		id = @prmId

END