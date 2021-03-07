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
            "Delete Department",
            "Get Department",
            "Get all Departments",
            "Update Department Name",
            "Update Department Manager",
            "Quit"
        };

        static CompanyContext ctx = new CompanyContext();
        static DepartmentService service = new DepartmentService(ctx);

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu() {
            Console.Clear();
            Console.WriteLine("Please type index for selected query");

            for (int i = 0; i < options.Count; i++) {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            var isValid = int.TryParse(Console.ReadLine(), out int choice);

            if (isValid) {
                Options(choice);
            } else {
                Console.WriteLine("The entered index was not valid");
                Console.ReadLine();
                Menu();
            }
        }

        static void Quit() {
            Environment.Exit(0);
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
                finally {
                    Console.WriteLine("Press a button to return to the menu");
                    Console.ReadLine();
                    Menu();
                }
            }
        }

        static void GetDepartment() {
            Console.WriteLine("Enter the id of the department");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);

            if(couldParse) {
                Console.WriteLine("Getting the department...");
                try {
                    var d = service.GetDepartment(dNumber);
                    Console.WriteLine($"Id: {d.DNumber} | Name: {d.DName} | MgrSSN: {d.MgrSSN} | MgrStartDate: {d.MgrStartDate} | EmpCount: {d.EmpCount}");
                } catch (SqlException ex) {

                    Console.WriteLine(ex.Message);
                } finally {
                    Console.WriteLine("Press a button to return to the menu");
                    Console.ReadLine();
                    Menu();
                }
            }
        }

        static void UpdateDepartmentName() {
            Console.WriteLine("Enter the id of the department you want to update the name on");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);
            Console.WriteLine("Enter a new name for the department");
            var dName = Console.ReadLine();

            if(couldParse && !String.IsNullOrEmpty(dName)) {
                Console.WriteLine("Updating the department...");
                try {
                    service.UpdateDepartmentName(dNumber, dName);
                } catch (SqlException ex) {

                    Console.WriteLine(ex.Message);
                } finally {
                    Console.WriteLine("Press a button to return to the menu");
                    Console.ReadLine();
                    Menu();
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
                } finally {
                    Console.WriteLine("Press a button to return to the menu");
                    Console.ReadLine();
                    Menu();
                }
            }
        }
        static void UpdateDepartmentManager() {
            Console.WriteLine("Enter the id of the Department you want to update the manager on");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);
            Console.WriteLine("Enter the mgrSSN for the new manager for the department");
            var couldParseSSN = decimal.TryParse(Console.ReadLine(), out decimal mgrSSN);

            if (couldParse && couldParseSSN) {
                Console.WriteLine("Updating the department manager...");
                try {
                   service.UpdateDepartmentManager(dNumber, mgrSSN);
                   Console.WriteLine("The department manager was updated");
                } catch (SqlException ex) {

                    Console.WriteLine(ex.Message);
                } finally {
                    Console.WriteLine("Press a button to return to the menu");
                    Console.ReadLine();
                    Menu();
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
                    GetDepartment();
                    break;
                case 4:
                    GetAllDepartments();
                    break;
                case 5: 
                    UpdateDepartmentName();
                    break;
                case 6:
                    UpdateDepartmentManager();
                    break;
                case 7:
                    Quit();
                    break;
                default:
                    Console.WriteLine("Choise does not exists");
                    break;
            }
        }

        private static void GetAllDepartments()
        {
            Console.WriteLine("Department Id | Department Name | Manager SSN");
            foreach (var department in service.GetAllDepartments())
            {
                Console.WriteLine($"{department.DNumber} | {department.DName} | {department.MgrSSN}");
            }
        }
    }
}
