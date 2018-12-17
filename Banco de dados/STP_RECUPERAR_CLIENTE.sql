USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 17/12/2018
-- Description:	Listar todos os cliente
-- =============================================
CREATE PROCEDURE [STP_RECUPERAR_CLIENTE] 

AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_cliente
	WHERE 
		ativo = 1  
	ORDER BY nome 
END