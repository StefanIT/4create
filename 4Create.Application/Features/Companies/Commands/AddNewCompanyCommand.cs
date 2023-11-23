using _4Create.Application.Common;
using _4Create.Application.Features.Employees.Models;
using _4Create.Application.Interfaces;
using _4Create.Application.Persistance;
using _4Create.Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _4Create.Application.Features.Companies.Commands
{
    public class AddNewCompanyCommand : IRequest<bool>
	{
        public required string CompanyName { get; set; }
        public List<EmployeeRequestModel> Employees { get; set; }
    }

	public class AddNewCompanyCommandValidator : BaseValidator<AddNewCompanyCommand>
	{
		public AddNewCompanyCommandValidator(IMySqlDbContext _context) 
		{
			RuleFor(x => x.CompanyName)
				   .NotEmpty()
				   .MaximumLength(250)
				   .Must(name => { return !_context.Companies.Any(x => x.Name == name); })
				   .WithMessage(x => $"Provided Company name: {x.CompanyName} must be unique.");
		}
	}

	public class AddNewCompanyCommandHandler : IRequestHandler<AddNewCompanyCommand, bool>
	{
		private readonly ICompanyService _companyService;
		private readonly IEmployeeService _employeeService;
		private readonly IMySqlDbContext _context;
		public AddNewCompanyCommandHandler(
			IMySqlDbContext context,
			IEmployeeService employeeService,
			ICompanyService companyService)
		{
			_context = context;
			_employeeService = employeeService;
			_companyService = companyService;
		}
		public async Task<bool> Handle(AddNewCompanyCommand request, CancellationToken cancellationToken)
		{
			var company = await _companyService.AddCompanyAsync(request);

			foreach(var newEployee in request.Employees)
			{
				Employee? employee = null;

				if (newEployee.EmployeeId.HasValue)
					employee = await _employeeService.GetEmployeeByIdAsync((int)newEployee.EmployeeId);

				else if (!string.IsNullOrEmpty(newEployee.EmployeeEmail))
					employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == newEployee.EmployeeEmail);

				if (employee is null)
				{
					employee = await _employeeService.AddEmployeeAsync(newEployee, cancellationToken);
					await _employeeService.AsingEmployeToCompanies(employee, new List<Company> { company });
				}
			}

			return await _context.SaveChangesAsync(true, cancellationToken) > 0;
		}
	}
}
