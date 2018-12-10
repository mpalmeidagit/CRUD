USE [CRUD]
GO
/****** Object:  StoredProcedure [dbo].[STP_VALIDAR_USUARIO]    Script Date: 09/12/2018 12:52:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 08/12/2018
-- Description:	Validar Usuários
-- =============================================
CREATE PROCEDURE [dbo].[STP_VALIDAR_USUARIO](
	@prmLogin varchar(50),
	@prmSenha varchar(32)
) 
AS
BEGIN
   
	SELECT * FROM tab_usuario WHERE login = @prmLogin AND senha = @prmSenha AND ativo = 1

END
