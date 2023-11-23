using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApiControllerBase : ControllerBase
	{
		private IMediator _mediator;
		protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
	}
}
