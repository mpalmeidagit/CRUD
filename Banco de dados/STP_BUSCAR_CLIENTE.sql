USE[CRUD]
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE STP_BUSCAR_CLIENTE(
	
	@prmBuscarCliente varchar(20)
)
AS
BEGIN
	SELECT
		c.id,
		c.nome,
		c.cpf,
		c.telefone
	FROM 
		tab_cliente c
	WHERE 
		c.ativo = 1 AND c.cpf = @prmBuscarPorCPF
END