USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 17/12/2018
-- Description:	Alterar cliente
-- =============================================
CREATE PROCEDURE STP_ALTERAR_CLIENTE(
	@prmId			int,
	@prmNome		varchar(100),
	@prmEmail		varchar(500),
	@prmCpf			varchar(20),
	@prmTelefone	varchar(20),
	@prmCep			varchar(12),
	@prmEstado		varchar(50),
	@prmCidade		varchar(50),
	@prmBairro		varchar(50),
	@prmEndereco	varchar(150)
)
AS
BEGIN
	IF EXISTS (SELECT id FROM tab_cliente WHERE email = @prmEmail AND id <> @prmId)
	BEGIN	
		RAISERROR('Já existe um cliente cadastrado com este email.', 16, 1)
		RETURN -1
	END
	IF EXISTS (SELECT id FROM tab_cliente WHERE cpf = @prmCpf AND id <> @prmId)
	BEGIN	
		RAISERROR('Já existe um cliente cadastrado com este cpf.', 16, 1)
		RETURN -1
	END
	UPDATE tab_cliente SET 
		nome		=  @prmNome,	
		email		=  @prmEmail,	
		cpf			=  @prmCpf,		
		telefone	=  @prmTelefone,
		cep			=  @prmCep,		
		estado		=  @prmEstado,	
		cidade		=  @prmCidade,	
		bairro		=  @prmBairro,	
		endereco	=  @prmEndereco	
				
	WHERE 
		id = @prmId
	SELECT SCOPE_IDENTITY();

END
GO
