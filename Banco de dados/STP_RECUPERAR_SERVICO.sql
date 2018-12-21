USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 17/12/2018
-- Description:	Listar todos os serviço
-- =============================================
CREATE PROCEDURE [STP_RECUPERAR_SERVICO] 

AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_servico
	WHERE 
		ativo = 1  
	ORDER BY descricao 
END