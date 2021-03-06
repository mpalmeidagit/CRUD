USE [CRUD]
GO
/****** Object:  StoredProcedure [dbo].[STP_ALTERAR_RECIBO]    Script Date: 24/12/2018 23:22:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[STP_ALTERAR_RECIBO](
	@prmId int,
	@prmData date,
	@prmServicoId int,
	@prmClienteId int
)
AS
BEGIN
	UPDATE tab_recibo SET 
		data = @prmData,
		servico_id = @prmServicoId,
		cliente_id = @prmClienteId			
	WHERE 
		id = @prmId AND ativo = 1
	SELECT SCOPE_IDENTITY();
END
