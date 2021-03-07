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

        // public void CreateDepartment(string dName, decimal mgrSSN)
        // {
        //     _ctx.Database.ExecuteSqlRaw("EXECUTE dbo.usp_CreateDepartment {0}, {1}", dName, mgrSSN);
        // }

        // public virtual List<Department> GetAllDepartments()
        // {
        //     return _ctx.Department.FromSqlRaw("EXECUTE dbo.usp_GetAllDepartments").ToList();
        // }
    }
}
