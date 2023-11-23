using _4Create.Application.Common;
using _4Create.Application.Exceptions;
using _4Create.Application.Features.Employees.Models;
using _4Create.Application.Interfaces;
using _4Create.Application.Persistance;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace _4Create.Application.Features.Employees.Commands
{
	public class AddNewEmployeeCommand : IRequest<bool>
	{
        public required string EmployeeEmail { get; set; }
        public required string EmployeeTitle { get; set; }
        public List<int> CompanyIds { get; set; } 
    }

	public class AddNewEmployeeCommandValidator : BaseValidator<AddNewEmployeeCommand>
	{
        public AddNewEmployeeCommandValidator(IMySqlDbContext _context)
        {
			RuleFor(x => x.EmployeeEmail)
				   .NotEmpty()
				   .EmailAddress()
				   .Must(email => { return !_context.Employees.Any(x => x.Email == email); })
				   .WithMessage(x => $"Provided employee email: {x.EmployeeEmail} must be unique.");
        }
    }

	public class AddNewEmployeeCommandHandler : IRequestHandler<AddNewEmployeeCommand, bool>
	{
		private readonly IEmployeeService _employeeService;
		private readonly IMySqlDbContext _context;
		public AddNewEmployeeCommandHandler(
			IMySqlDbContext context,
			IEmployeeService employeeService)
		{
			_context = context;
			_employeeService = employeeService;
		}
		public async Task<bool> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
		{
			var newEmployee = await _employeeService.AddEmployeeAsync(new EmployeeRequestModel(null, request.EmployeeEmail, request.EmployeeTitle), cancellationToken);


			var existingCompanies = await _context.Companies
													.Include(x => x.EmployeeCompanyValues)
													.ThenInclude(x => x.Employee)
													.Where(x => request.CompanyIds.Contains(x.Id))
													.ToListAsync(cancellationToken);

			if (!existingCompanies.Any())
				throw new NotFoundException("Company", string.Join(',', request.CompanyIds));

			var newEmployeeAssociateCompanies = _employeeService.AsingEmployeToCompanies(newEmployee, existingCompanies);
			await newEmployeeAssociateCompanies;

			return await _context.SaveChangesAsync(true, cancellationToken) > 0;
		}
	}
}
