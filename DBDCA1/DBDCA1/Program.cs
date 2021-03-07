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
            "Get Department",
            "Update DepartmentName",
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
                Options(choice);
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

        static void GetDepartment(DepartmentService service ) {
            Console.WriteLine("Enter the id of the department");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);

            if(couldParse) {
                Console.WriteLine("Getting the department...");
                try {
                    var d = service.GetDepartment(dNumber);
                    Console.WriteLine($"Id: {d.DNumber} | Name: {d.DName} | MgrSSN: {d.MgrSSN} | MgrStartDate: {d.MgrStartDate} | NoOfEmployees: {d.NoOfEmployees}");
                } catch (SqlException ex) {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void UpdateDepartmentName(DepartmentService service) {
            Console.WriteLine("Enter the id of the department you want to update the name on ");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);
            Console.WriteLine("Enter a new name for the department");
            var dName = Console.ReadLine();

            if(couldParse && !String.IsNullOrEmpty(dName)) {
                Console.WriteLine("Updating the department...");
                try {
                    service.UpdateDepartmentName(dNumber, dName);
                } catch (SqlException ex) {

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

        static void Options(int option)
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
                    GetDepartment(service);
                    break;
                case 4:
                    UpdateDepartmentName(service);
                    break;
                default:
                    Console.WriteLine("Choise does not exists");
                    break;
            }
        }
    }
}
