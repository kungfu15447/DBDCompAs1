CREATE PROCEDURE [usp_GetDepartment]
	@DNumber int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT d.DNumber, d.DName, d.MgrSSN, d.MgrStartDate, count(e.SSN) as noOfEmployees
	FROM Department as d LEFT JOIN Employee as e ON e.Dno = d.DNumber
	WHERE  d.DNumber = @DNumber
	GROUP BY d.DNumber, d.DName, d.MgrSSN, d.MgrStartDate;

END