USE[CRUD]
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE STP_RECUPERAR_RECIBO
AS
BEGIN
	SELECT
		r.id,	
		r.data,
		s.descricao,
		s.valor,
		c.nome,
		c.cpf,
		c.telefone,
		r.cliente_id,
		r.servico_id
	FROM 
		tab_recibo r
		INNER JOIN tab_servico s ON (r.servico_id = s.id)
		INNER JOIN tab_cliente c ON (r.cliente_id = c.id)
	WHERE r.ativo = 1  

END
GO
