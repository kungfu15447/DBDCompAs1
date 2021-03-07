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
            "Create Department",
            "See all the Departments with count of Employees",
            "Delete Department"
        };

        static CompanyContext ctx = new CompanyContext();
        static DepartmentService service = new DepartmentService(ctx);

        static void Main(string[] args)
        {
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

        static void CreateDepartment()
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
        }

        static void DeleteDepartment()
        {
            Console.WriteLine("Enter number of department to delete");
            var isNumber = int.TryParse(Console.ReadLine(), out int dNumber);

            if (isNumber)
            {
                Console.WriteLine("Deleting department");
                try
                {
                    service.DeleteDepartment(dNumber);
                    Console.WriteLine("Deleted succesfully");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Options(int option, DepartmentService service)
        {
            switch(option)
            {
                case 1:
                    CreateDepartment();
                    break;
                case 2:
                    DeleteDepartment();
                    break;
                case 3:
                    GetAllDepartments(service);
                    break;
                default:
                    Console.WriteLine("Choise does not exists");
                    break;
            }
        }

        private static void GetAllDepartments(DepartmentService service)
        {
            Console.WriteLine("Department Id | Department Name | Manager SSN");
            foreach (var department in service.GetAllDepartments())
            {
                Console.WriteLine($"{department.DNumber} | {department.DName} | {department.MgrSSN}");
            }
        }
    }
}
