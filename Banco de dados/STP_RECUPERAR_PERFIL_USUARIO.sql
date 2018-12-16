USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 16/12/2018
-- Description:	Selecionar perfil dos usuários
-- =============================================
CREATE PROCEDURE STP_RECUPERAR_PERFIL_USUARIO(
	
	@prmIdUsuario int
)
AS
BEGIN
	SELECT 
		p.nome 
	FROM 
		tab_perfil_usuario pu, tab_perfil p 
	WHERE 
		pu.id_perfil = @prmIdUsuario AND pu.id_perfil = p.id AND p.ativo = 1
END
GO
