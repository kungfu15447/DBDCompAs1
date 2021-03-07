USE [Company]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [UpdateDepartmentName]
	@DNumber int,
	@DName varchar(50)
AS
BEGIN
SET NOCOUNT ON;

Exec ThrowIfNotUniqueDName @DName = @DName

	UPDATE Department 
	SET  DName = @DName
	WHERE DNumber = @DNumber

END