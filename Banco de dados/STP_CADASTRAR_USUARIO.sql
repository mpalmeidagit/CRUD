USE [CRUD]
GO

-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 09/12/2018
-- Description:	Cadastrar usuario
-- =============================================
CREATE PROCEDURE [dbo].[STP_CADASTRAR_USUARIO](
	@prmLogin varchar(50),
	@prmSenha varchar(32),
	@prmNome varchar(100),
	@prmEmail varchar(100),
	@prmIdPerfil int
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_usuario WHERE email = @prmEmail)
	BEGIN	
		RAISERROR('Já existe um usuário cadastrado com este email.', 16, 1)
		RETURN -1
	END
	IF EXISTS (SELECT id FROM tab_usuario WHERE login = @prmLogin)
	BEGIN	
		RAISERROR('Já existe um usuário cadastrado com este login.', 16, 1)
		RETURN -1
	END
	INSERT INTO tab_usuario
	(
		login,
		senha,
		nome,
		email,
		id_perfil,		
		ativo
	)VALUES(
		@prmLogin,
		@prmSenha,
		@prmNome,
		@prmEmail,
		@prmIdPerfil,
		1
	)
	SELECT SCOPE_IDENTITY();
END


