USE [Company]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usp_UpdateDepartmentName]
	@DNumber int,
	@DName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

	UPDATE Department 
	SET  DName = @DName
	WHERE DNumber = @DNumber

END