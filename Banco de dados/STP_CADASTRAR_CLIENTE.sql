USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 17/12/2018
-- Description:	Cadastrar Cliente
-- =============================================
CREATE PROCEDURE STP_CADASTRAR_CLIENTE(
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
	IF EXISTS (SELECT id FROM tab_cliente WHERE email = @prmEmail)
	BEGIN	
		RAISERROR('Já existe um cliente cadastrado com este email.', 16, 1)
		RETURN -1
	END
	IF EXISTS (SELECT id FROM tab_cliente WHERE cpf = @prmCpf)
	BEGIN	
		RAISERROR('Já existe um cliente cadastrado com este cpf.', 16, 1)
		RETURN -1
	END
	INSERT INTO tab_cliente
	(
		nome,
		email,
		cpf,
		telefone,
		cep,
		estado,
		cidade,
		bairro,
		endereco,
		ativo
	)VALUES(
		@prmNome,	
		@prmEmail,	
		@prmCpf,		
		@prmTelefone,
		@prmCep,		
		@prmEstado,
		@prmCidade,	
		@prmBairro,	
		@prmEndereco,
		1
	)
	SELECT SCOPE_IDENTITY();
END
GO
		