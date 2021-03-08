CREATE PROCEDURE usp_UpdateDepartmentManager
(
    @DNumber int,
    @MgrSSN numeric(9, 0)
)
AS
BEGIN 

	UPDATE Department
	SET MgrSSN = @MgrSSN, MgrStartDate = GetDate()
	WHERE DNumber = @DNumber;

	UPDATE Employee
	SET SuperSSN = @MgrSSN
	WHERE (DNo = @DNumber) AND (SSN <> @MgrSSN);

END