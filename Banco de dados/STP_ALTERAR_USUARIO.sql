USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 11/12/2018
-- Description:	Alterar usuário
-- =============================================
CREATE PROCEDURE STP_ALTERAR_USUARIO(
	@prmId int,
	@prmNome varchar(100),
	@prmEmail varchar(100),
	@prmLogin varchar(50)
	--@prmSenha varchar(32)
)
AS
BEGIN

	UPDATE tab_usuario SET 
		nome = @prmNome, 
		email = @prmEmail, 
		login = @prmLogin
		--senha = @prmSenha 
	WHERE 
		id = @prmId

END
GO
