-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
USE[CRUD]
GO
-- =============================================
-- Author:		Marivaldo Almeida
-- Create date: 11/12/2018
-- Description:	Remover usuáario
-- =============================================
CREATE PROCEDURE STP_REMOVER_USUARIO(
	@prmId int
) 
AS
BEGIN

	DELETE FROM 
		tab_usuario 
	WHERE id = @prmId

END
GO
