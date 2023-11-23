using _4Create.Application.Features.Employees.Commands;
using _4Create.Application.Features.Employees.Models;
using _4Create.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application.Interfaces
{
	public interface IEmployeeService
	{
		Task<Employee> GetEmployeeByIdAsync(int id);
		Task<Employee> AddEmployeeAsync(EmployeeRequestModel employee, CancellationToken cancellationToken);
		Task AsingEmployeToCompanies(Employee employee, IEnumerable<Company> companies);
	}
}
