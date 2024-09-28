using Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification.Employee_Specs
{
    public class EmployeeWithDepartmentSpecification:BaseSpecifications<Employee>
    {
        public EmployeeWithDepartmentSpecification():base()
        {
            AddIncludes();
        }
        public EmployeeWithDepartmentSpecification(int id):base(p=>p.Id==id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(p => p.Departments);
        }
    }
}
