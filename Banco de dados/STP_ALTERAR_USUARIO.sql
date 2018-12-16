USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 11/12/2018
-- Description:	Alterar usuário
-- =============================================
CREATE PROCEDURE [dbo].[STP_ALTERAR_USUARIO](
	@prmId int,
	@prmNome varchar(100),
	@prmEmail varchar(100),
	@prmLogin varchar(50),
	@prmIdPerfil int
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_usuario WHERE email = @prmEmail AND id <> @prmId)
	BEGIN	
		RAISERROR('Já existe um usuário cadastrado com este email.', 16, 1)
		RETURN -1
	END
	IF EXISTS (SELECT id FROM tab_usuario WHERE login = @prmLogin AND id <> @prmId)
	BEGIN	
		RAISERROR('Já existe um usuário cadastrado com este login.', 16, 1)
		RETURN -1
	END
	UPDATE tab_usuario SET 
		nome = @prmNome, 
		email = @prmEmail, 
		login = @prmLogin,
		id_perfil = @prmIdPerfil		
	WHERE 
		id = @prmId
	SELECT SCOPE_IDENTITY();
END