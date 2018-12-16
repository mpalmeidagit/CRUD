USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 16/16/2018
-- Description:	Cadastrar perfil
-- =============================================
CREATE PROCEDURE STP_CADASTRAR_PERFIL(
	@prmNome varchar(100)
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_perfil WHERE nome = @prmNome)
	BEGIN	
		RAISERROR('Já existe um perifl cadastrado com este nome', 16, 1)
		RETURN -1
	END
	INSERT INTO tab_perfil
	(	
		nome,		
		ativo
	)VALUES(	
		@prmNome,
		1
	)
	SELECT SCOPE_IDENTITY();
END
