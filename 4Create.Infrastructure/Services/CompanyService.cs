using _4Create.Application.Exceptions;
using _4Create.Application.Features.Companies.Commands;
using _4Create.Application.Persistance;
using _4Create.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Infrastructure.Services
{
	public class CompanyService : ICompanyService
	{
		private readonly IMySqlDbContext _context;
		public CompanyService(IMySqlDbContext context)
		{
			_context = context;
		}
		public async Task<Company> AddCompanyAsync(AddNewCompanyCommand company)
		{
			if (await _context.Companies.AnyAsync(c => c.Name == company.CompanyName))
				throw new NotValidException($"Company name {company.CompanyName} is not unique.");

			var newCompany = new Company
			{
				Name = company.CompanyName
			};

			await _context.Companies.AddAsync(newCompany);
			return newCompany;
		}
	}
}
