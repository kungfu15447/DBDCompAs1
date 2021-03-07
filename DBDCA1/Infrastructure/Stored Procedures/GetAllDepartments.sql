CREATE PROCEDURE usp_GetAllDepartments
AS 
BEGIN

SELECT d.DNumber, d.DName, d.MgrSSN, d.MgrStartDate, COUNT(e.SSN) AS EmpCount
    FROM Department AS d 
        LEFT JOIN Employee AS e ON e.DNo = d.DNumber;

END
