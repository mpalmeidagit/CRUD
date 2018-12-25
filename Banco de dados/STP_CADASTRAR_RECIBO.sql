USE[CRUD]
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE STP_CADASTRAR_RECIBO(
	
	@prmData date,
	@prmServicoId int,
	@prmClienteId int

)
AS
BEGIN
	INSERT INTO tab_recibo
	(
		data,
		servico_id,
		cliente_id,	
		ativo
	)VALUES(
		@prmData,
		@prmServicoId,
		@prmClienteId,				
		1
	)
	SELECT SCOPE_IDENTITY();
END
GO
