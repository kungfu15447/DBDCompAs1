USE [Company]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_UpdateDepartmentManager
(
    @DNumber int,
    @MgrSSN numeric(9, 0)
)
AS
BEGIN 

	UPDATE Department
	SET MgrSSN = @MgrSSN, MgrStartDate = CURRENT_TIMESTAMP
	WHERE DNumber = @DNumber;

	UPDATE Employee
	SET SuperSSN = @MgrSSN
	WHERE (DNo = @DNumber) AND (SSN <> @MgrSSN);

END