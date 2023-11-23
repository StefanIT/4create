using _4Create.Application.Features.Companies.Commands;
using _4Create.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.API.Controllers
{
	public class CompanyController : ApiControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateEmployee([FromBody] AddNewCompanyCommand command) => Ok(ResponseModel.Create(await Mediator.Send(command)));
	}
}
