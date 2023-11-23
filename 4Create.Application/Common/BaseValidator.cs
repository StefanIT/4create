using _4Create.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace _4Create.Application.Common
{
	public class BaseValidator<T> : AbstractValidator<T> where T : class
	{
		public override ValidationResult Validate(ValidationContext<T> context)
		{
			var result = base.Validate(context);

			if (!result.IsValid)
				throw new NotValidException(string.Join(Environment.NewLine, result.Errors.Select(x => $"{x.PropertyName} - {x.ErrorMessage}")));

			return result;
		}
	}
}
