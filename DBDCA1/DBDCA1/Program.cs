using Infrastructure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        static DepartmentService service;

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            service = new DepartmentService(new CompanyContext());
            Console.Clear();
            Console.WriteLine("Please type index for selected query");

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            var isValid = int.TryParse(Console.ReadLine(), out int choice);

            if (isValid)
            {
                Options(choice);
            }
            else
            {
                Console.WriteLine("The entered index was not valid - press a key to try again");
                Console.ReadLine();
                Menu();
            }
        }

        static void Quit()
        {
            Environment.Exit(0);
        }

        static void BackToMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Press a button to return to the menu...");
            Console.ReadLine();
            Menu();
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

        static void GetDepartment()
        {
            Console.WriteLine("Enter the id of the department");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);

            if (couldParse)
            {
                Console.WriteLine("Getting the department...");
                try
                {
                    var d = service.GetDepartment(dNumber);
                    if (d != null)
                    {
                        Console.WriteLine(
                            $"Id: {d.DNumber} | Name: {d.DName} | MgrSSN: {d.MgrSSN} | MgrStartDate: {d.MgrStartDate} | EmpCount: {d.EmpCount}");
                    }
                    else
                    {
                        Console.WriteLine("Could not find department");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void UpdateDepartmentName()
        {
            Console.WriteLine("Enter the id of the department you want to update the name on");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);
            Console.WriteLine("Enter a new name for the department");
            var dName = Console.ReadLine();

            if (couldParse && !String.IsNullOrEmpty(dName))
            {
                Console.WriteLine("Updating the department...");
                try
                {
                    service.UpdateDepartmentName(dNumber, dName);
                    Console.WriteLine("Succesfully updated department name");
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

        static void UpdateDepartmentManager()
        {
            Console.WriteLine("Enter the id of the Department you want to update the manager on");
            var couldParse = int.TryParse(Console.ReadLine(), out int dNumber);
            Console.WriteLine("Enter the mgrSSN for the new manager for the department");
            var couldParseSSN = decimal.TryParse(Console.ReadLine(), out decimal mgrSSN);

            if (couldParse && couldParseSSN)
            {
                Console.WriteLine("Updating the department manager...");
                try
                {
                    service.UpdateDepartmentManager(dNumber, mgrSSN);
                    Console.WriteLine("The department manager was updated");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Options(int option)
        {
            var willStop = false;
            switch (option)
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
                    willStop = true;
                    break;
                default:
                    Console.WriteLine("Choise does not exists");
                    break;
            }

            if (!willStop)
            {
                BackToMenu();
            }
        }

        private static void GetAllDepartments()
        {
            Console.WriteLine("|_Id_|_Name_____________|_Manager SSN_|_Manager Start Date_|_Total Employees_|");
            foreach (var d in service.GetAllDepartments())
            {
                Console.WriteLine($"{ToTableString(2, 4, d.DNumber.ToString())}" +
                                  $"{ToTableString(2, 18, d.DName)}" +
                                  $"{ToTableString(2,13,d.MgrSSN.ToString())}" +
                                  $"{ToTableString(2, 20, d.MgrStartDate.ToShortDateString())}" +
                                  $"{ToTableString(2, 17, d.EmpCount.Value.ToString(), true)}");
            }
        }

        private  static string ToTableString(int startIndex, int endOfHeaderline, string toWrite, bool isLast = false)
        {
            
                var newString = "";
                var toWriteLength = toWrite.Length;
                int count = 0;
                for (int i = 0; i < endOfHeaderline; i++)
                {
                    if (i == 0)
                    {
                        newString = newString + "|";
                    }

                    if (i + 1 < startIndex)
                    {
                        newString = newString + "_";
                    }
                    else
                    {
                        if (toWriteLength > count)
                        {
                            newString = newString + toWrite[count];
                            count++;
                        }
                        else
                        {
                            newString = newString + "_";
                        }
                    }

                    if (isLast)
                    {
                        if (endOfHeaderline - 1 == i)
                        {
                            newString = newString + "|";
                        }
                    }
                }

                return newString;

        }
    }
}