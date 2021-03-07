using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class Department
    {
        public int DNumber { get; set; }
        public string DName { get; set; }
        public DateTime MgrStartDate { get; set; }
        public decimal MgrSSN { get; set; }
        public int? NoOfEmployees { get; set; }
    }
}
