USE [CRUD]
GO

-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 09/12/2018
-- Description:	Cadastrar usuario
-- =============================================
CREATE PROCEDURE [dbo].[STP_CADASTRAR_USUARIO](
	@prmNome varchar(100),
	@prmEmail varchar(100),
	@prmLogin varchar(50),
	@prmSenha varchar(32)	
)
AS
BEGIN
	INSERT INTO tab_usuario
	(
		nome,
		email,
		login,
		senha,		
		ativo
	)VALUES(
		@prmNome,
		@prmEmail,
		@prmLogin,
		@prmSenha,		
		1
	)
	SELECT SCOPE_IDENTITY();
END
