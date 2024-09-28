using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specification.Employee_Specs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_E_CommerceProject.Api.Controllers;

namespace Test_E_CommerceProject.Service.Controllers
{

    public class EmployeesController : BaseApiController
    {
        private readonly IGenericRepository<Employee> employeeRepo;

        public EmployeesController(IGenericRepository<Employee> EmployeeRepo)
        {
            employeeRepo = EmployeeRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            var spec = new EmployeeWithDepartmentSpecification();
            return await employeeRepo.GetAllWithSpecAsync(spec); 
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int Id)
        {
            var spec = new EmployeeWithDepartmentSpecification(Id);
            var employee= employeeRepo.GetEntityWithSpecAsync(spec);
            if (employee != null) 
                return Ok(employee);
            else
                return BadRequest();
        }

    }
}
