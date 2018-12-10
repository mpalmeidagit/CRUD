USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 09/12/2018
-- Description:	Cadastrar usuario
-- =============================================
CREATE PROCEDURE STP_CADASTRAR_USUARIO(
	@prmLogin varchar(50),
	@prmSenha varchar(32),
	@prmNome varchar(100),
	@prmEmail varchar(100)
)
AS
BEGIN
	INSERT INTO tab_usuario
	(
		login,
		senha,
		nome,
		email,
		ativo
	)VALUES(
		@prmLogin,
		@prmSenha,
		@prmNome,
		@prmEmail,
		1
	)
	SELECT SCOPE_IDENTITY() AS Retorno;
END
GO
