CREATE FUNCTION udf_GetEmployeesInDepartment
(
	@DNumber int
)
RETURNS INT
AS
BEGIN
	DECLARE @EmpCount INT;
	SELECT @EmpCount = COUNT(*) 
	FROM Employee
	WHERE Dno = @DNumber
	RETURN @EmpCount;
END