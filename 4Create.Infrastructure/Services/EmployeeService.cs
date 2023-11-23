using _4Create.Application.Exceptions;
using _4Create.Application.Features.Employees.Commands;
using _4Create.Application.Features.Employees.Models;
using _4Create.Application.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Infrastructure.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IMySqlDbContext _context;
		public EmployeeService(IMySqlDbContext context)
		{
			_context = context;
		}
		public async Task<Employee> AddEmployeeAsync(EmployeeRequestModel employee, CancellationToken cancellationToken)
		{
			var correctTitleType = Enum.TryParse(typeof(TitleTypeEnum), employee.EmployeeTitle, out var employeeTitle);

			if (!correctTitleType)
				throw new NotValidException($"Employee title {employee.EmployeeTitle} is not correct.");

			var newEmployee = new Employee
			{
				Email = employee.EmployeeEmail,
				TitleId = (int)employeeTitle
			};

			await _context.Employees.AddAsync(newEmployee, cancellationToken);

			return newEmployee;
		}

		public async Task AsingEmployeToCompanies(Employee employee, IEnumerable<Company> companies)
		{
			foreach (var company in companies)
			{
				if (company.EmployeeCompanyValues.Select(x => x.Employee).Any(x => x.TitleId == employee.TitleId))
					throw new NotValidException($"There could not be 2 employees with the title {employee.TitleId}");

				var employeeCompany = new EmployeeCompanyValue
				{
					Company = company,
					Employee = employee
				};

				await _context.EmployeeCompanyValues.AddAsync(employeeCompany);
			}
			
		}

		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
