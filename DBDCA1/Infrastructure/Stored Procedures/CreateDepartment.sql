CREATE PROCEDURE usp_CreateDepartment
(
	@DName varchar(50),
	@MgrSSN numeric(9, 0)
)
AS
BEGIN
	INSERT INTO Department (DName, MgrSSN, MgrStartDate)
	VALUES (@DName, @MgrSSN, GETDATE())
END