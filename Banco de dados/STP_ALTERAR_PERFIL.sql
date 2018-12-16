USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 16/12/2018
-- Description:	Alterar perfil
-- =============================================
CREATE PROCEDURE STP_ALTERAR_PERFIL(
	@prmId int,
	@prmNome varchar(100)
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_perfil WHERE nome = @prmNome AND id <> @prmId)
	BEGIN	
		RAISERROR('Já existe um perfil cadastrado com este nome.', 16, 1)
		RETURN -1
	END
	UPDATE tab_perfil SET 
		nome = @prmNome	
	WHERE 
		id = @prmId
	SELECT SCOPE_IDENTITY();
END
