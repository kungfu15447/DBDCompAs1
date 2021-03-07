using Infrastructure;
using Microsoft.Data.SqlClient;
using System;

namespace DBDCA1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CompanyContext();
            var service = new DepartmentService(ctx);

            Console.WriteLine("Enter name of department");
            var dName = Console.ReadLine();
            Console.WriteLine("Enter SSN of manager");
            var couldParse = decimal.TryParse(Console.ReadLine(), out decimal mgrSSN);

            if (couldParse && !String.IsNullOrEmpty(dName))
            {
                Console.WriteLine("Writing to database");
                try
                {
                    service.CreateDepartment(dName, mgrSSN);
                    Console.WriteLine("Finished writing");
                } catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }
    }
}
