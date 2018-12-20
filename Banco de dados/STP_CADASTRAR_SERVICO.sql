USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Cadastrar serviço
-- =============================================
CREATE PROCEDURE STP_CADASTRAR_SERVICO(
	@prmDescricao varchar(100),
	@prmValor decimal
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_servico WHERE descricao = @prmDescricao)
	BEGIN	
		RAISERROR('Já existe um serviço cadastrado com esta descrição.', 16, 1)
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
