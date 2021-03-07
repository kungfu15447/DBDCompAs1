﻿using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class DepartmentService
    {
        private CompanyContext _ctx;

        public DepartmentService(CompanyContext ctx)
        {
            _ctx = ctx;
        }

        public void CreateDepartment(string dName, decimal mgrSSN)
        {
            _ctx.Database.ExecuteSqlRaw("EXECUTE dbo.usp_CreateDepartment {0}, {1}", dName, mgrSSN);
        }

        public void DeleteDepartment(int dNumber)
        {
            _ctx.Database.ExecuteSqlRaw("EXECUTE dbo.usp_DeleteDepartment {0}", dNumber);
        }

        public List<Department> GetAllDepartments()
        {
            return _ctx.Department.ToList();
        }

        public Department GetDepartment(int dNumber) {
            return _ctx.Department.FromSqlRaw<Department>("EXECUTE dbo.usp_GetDepartment {0}", dNumber).ToList().FirstOrDefault();

        }
        
        public void UpdateDepartmentName(int dNumber, string dName) {
            _ctx.Database.ExecuteSqlRaw("EXECUTE dbo.usp_UpdateDepartmentName {0}, {1}", dNumber, dName);
        }
    }
}
