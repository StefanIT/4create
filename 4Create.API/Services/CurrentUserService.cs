using _4Create.Application.Interfaces;
using System.Security.Claims;

namespace _4Create.API.Services
{
	public class CurrentUserService : ICurrentUserService
	{
        private readonly IHttpContextAccessor _contextAccessor;
		public CurrentUserService(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}
		public string UserEmail => _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Upn) ?? "N/A";
	}
}
