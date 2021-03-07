CREATE PROCEDURE [ThrowIfNotUniqueDName]
	@DName varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	if @DName in ( SELECT DName from dbDepartment)
		RaisError('Department Name already exist',16,1)
END