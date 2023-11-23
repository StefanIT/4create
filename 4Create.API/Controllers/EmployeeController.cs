using _4Create.Application.Features.Employees.Commands;
using _4Create.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.API.Controllers
{
	//[Authorize]
	public class EmployeeController : ApiControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateEmployee([FromBody] AddNewEmployeeCommand command) => Ok(ResponseModel.Create(await Mediator.Send(command)));
	}
}
