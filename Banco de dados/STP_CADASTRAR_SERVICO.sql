USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Cadastrar servi�o
-- =============================================
CREATE PROCEDURE STP_CADASTRAR_SERVICO(
	@prmDescricao varchar(100),
	@prmValor decimal
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_servico WHERE descricao = @prmDescricao)
	BEGIN	
		RAISERROR('J� existe um servi�o cadastrado com esta descri��o.', 16, 1)
		RETURN -1
	END
	INSERT INTO tab_servico
	(
		descricao,
		valor,
		ativo
	)VALUES(
		@prmDescricao,
		@prmValor,
		1
	)
	SELECT SCOPE_IDENTITY();
END
GO
