use[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 11/12/2018
-- Description:	Selecionar todos os usuários
-- =============================================
CREATE PROCEDURE STP_RECUPERAR_USUARIO 

AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_usuario 	
	WHERE 
		ativo = 1  
	ORDER BY nome 
END
GO
