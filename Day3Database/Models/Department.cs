using System;
using System.Collections.Generic;
using System.Text;

namespace Day3Database.Models
{
    public class Department
    {
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }

    }
}
