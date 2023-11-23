using _4Create.Application.Features.Companies.Commands;
using _4Create.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.API.Controllers
{
	//[Authorize]
	public class CompanyController : ApiControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateEmployee([FromBody] AddNewCompanyCommand command) => Ok(ResponseModel.Create(await Mediator.Send(command)));
	}
}
