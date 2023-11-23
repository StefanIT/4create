using _4Create.Application.Exceptions;
using _4Create.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace _4Create.API.Attributes
{
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

		public ApiExceptionFilterAttribute()
		{
			_exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
			{
				{ typeof(NotValidException), HandleNotValidException },
				{ typeof(NotFoundException), HandleNotFoundException },
				{ typeof(UnauthorizedAccessException), HandleAuthorizationException },
			};
		}

		public override void OnException(ExceptionContext context)
		{
			HandleException(context);
			base.OnException(context);
		}

		private void HandleException(ExceptionContext context)
		{
			var type = context.Exception.GetType();
			if (_exceptionHandlers.ContainsKey(type))
			{
				_exceptionHandlers[type].Invoke(context);
				return;
			}

			if (!context.ModelState.IsValid)
			{
				HandleInvalidModelStateException(context);
				return;
			}

			HandleUnknownException(context);
		}

		private void HandleUnknownException(ExceptionContext context)
		{
			context.Result = new ObjectResult(new ErrorResponse
			{
				Code = HttpStatusCode.InternalServerError,
				Errors = new List<ErrorModel> { new ErrorModel
				{
					Name = "Internal Server Error",
					Message = "Internal Server Error"
				}}
			})
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};

			context.ExceptionHandled = true;
		}

		private void HandleInvalidModelStateException(ExceptionContext context)
		{
			context.Result = new BadRequestObjectResult(new ErrorResponse
			{
				Code = HttpStatusCode.BadRequest,
				Errors = new List<ErrorModel> { new ErrorModel
				{
					Name = "Bad Request",
					Message = context.Exception.Message
				}}
			})
			{
				StatusCode = StatusCodes.Status400BadRequest
			};

			context.ExceptionHandled = true;
		}

		private void HandleNotFoundException(ExceptionContext context)
		{
			context.Result = new NotFoundObjectResult(new ErrorResponse
			{
				Code = HttpStatusCode.NotFound,
				Errors = new List<ErrorModel> { new ErrorModel
				{
					Name = "The specified resource was not found.",
					Message = context.Exception.Message
				}}
			})
			{
				StatusCode = StatusCodes.Status404NotFound
			};

			context.ExceptionHandled = true;
		}

		private void HandleAuthorizationException(ExceptionContext context)
		{
			context.Result = new ObjectResult(new ErrorResponse
			{
				Code = HttpStatusCode.Unauthorized,
				Errors = new List<ErrorModel> { new ErrorModel
				{
					Name = "User Authorization failed!",
					Message = "User Authorization failed!"
				}}
			})
			{
				StatusCode = StatusCodes.Status401Unauthorized
			};

			context.ExceptionHandled = true;
		}

		private void HandleNotValidException(ExceptionContext context)
		{
			context.Result = new BadRequestObjectResult(new ErrorResponse
			{
				Code = HttpStatusCode.BadRequest,
				Errors = new List<ErrorModel> { new ErrorModel
				{
					Name = "Bad Request",
					Message = context.Exception.Message
				}}
			})
			{
				StatusCode = StatusCodes.Status400BadRequest
			};

			context.ExceptionHandled = true;
		}
	}
}
