
namespace _4Create.Infrastructure.Services
{
	public class DataTimeService : IDataTimeService
	{
		public DateTime Now => DateTime.UtcNow;
	}
}
