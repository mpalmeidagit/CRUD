USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 15/12/2018
-- Description:	Listar Perfil
-- =============================================
CREATE PROCEDURE STP_RECUPERAR_PERFIL

AS
BEGIN
	SELECT 
		* 
	FROM 
		tab_perfil 	
	WHERE 
		ativo = 1  
	ORDER BY nome 
END
GO
