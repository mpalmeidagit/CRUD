USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 17/12/2018
-- Description:	Listar cliente por ID
-- =============================================
CREATE PROCEDURE [STP_RECUPERAR_CLIENTE_PELO_ID](
	@prmId int
)
AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_cliente
	WHERE 
		id = @prmId and ativo = 1
END

