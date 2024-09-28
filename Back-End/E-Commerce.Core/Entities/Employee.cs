using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }

        ///relationship One To many In Side Department
        //Foreign Key
        public int DepartmentId { get; set; }
        //navigational Property
        public Department Departments { get; set; }
    
    }
}
