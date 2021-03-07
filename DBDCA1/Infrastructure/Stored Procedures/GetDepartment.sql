CREATE PROCEDURE [usp_GetDepartment]
	@DNumber int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Department
	WHERE DNumber = @DNumber
END