using _4Create.Application.Features.Companies.Commands;
using _4Create.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application.Interfaces
{
	public interface ICompanyService
	{
		Task<Company> AddCompanyAsync(AddNewCompanyCommand company);
	}
}
