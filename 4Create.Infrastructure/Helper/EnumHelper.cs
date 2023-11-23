using System.ComponentModel;
using System.Reflection;

namespace _4Create.Infrastructure.Helper
{
	public static class EnumHelper
	{
		public static string GetDescription(this Enum value)
		{
			return value.GetType()
						.GetMember(value.ToString())
						.First()
						.GetCustomAttribute<DescriptionAttribute>()?
						.Description ?? string.Empty;
		}
	}
}
