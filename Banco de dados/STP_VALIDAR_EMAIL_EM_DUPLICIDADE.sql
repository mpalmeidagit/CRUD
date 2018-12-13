USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 13/12/2018
-- Description:	Validar e-mail em duplicidade
-- =============================================
CREATE PROCEDURE STP_VALIDAR_EMAIL_EM_DUPLICIDADE(
	
	@prmEmail varchar(100)
)
AS
BEGIN
	SELECT email FROM tab_usuario WHERE email = @prmEmail
END
GO
