USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 20/12/2018
-- Description:	Alterar serviço
-- =============================================
CREATE PROCEDURE STP_ALTERAR_SERVICO(
	@prmId int,
	@prmDescricao varchar(100),
	@prmValor decimal
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_servico WHERE descricao = @prmDescricao AND id <> @prmId)
	BEGIN	
		RAISERROR('Já existe um serviço cadastrado com esta descrição.', 16, 1)
		RETURN -1
	END
	UPDATE tab_servico SET 
		descricao	=  @prmDescricao,	
		valor		=  @prmValor			
	WHERE 
		id = @prmId AND ativo = 1

	SELECT SCOPE_IDENTITY();
END
GO
