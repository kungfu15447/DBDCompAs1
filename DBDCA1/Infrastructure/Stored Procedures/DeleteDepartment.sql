CREATE PROCEDURE usp_DeleteDepartment
(
	@DNumber int
)
AS
BEGIN
	DELETE FROM Department WHERE DNumber = @DNumber
END