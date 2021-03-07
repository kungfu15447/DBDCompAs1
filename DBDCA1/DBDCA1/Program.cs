using Infrastructure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace DBDCA1
{
    public class Program
    {
        static List<string> options = new List<string>()
        {
            "Create Department"
        };
        static void Main(string[] args)
        {
            var ctx = new CompanyContext();
            var service = new DepartmentService(ctx);

            Console.WriteLine("Please type index for selected query");

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i+1}. {options[i]}");
            }

            var isValid = int.TryParse(Console.ReadLine(), out int choice);

            if (isValid)
            {
                Options(choice, service);
            } else
            {
                Console.WriteLine("Not valid");
            }
        }

        static void CreateDepartment(DepartmentService service)
        {
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
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        static void Options(int option, DepartmentService service)
        {
            switch(option)
            {
                case 1:
                    CreateDepartment(service);
                    break;
                default:
                    Console.WriteLine("Choise does not exists");
                    break;
            }
        }
    }
}
