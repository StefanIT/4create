using System.ComponentModel.DataAnnotations;

namespace _4Create.Domain.Models
{
	public class Company : BaseEntity
	{
		[StringLength(250)]
		public required string Name { get; set; }

		public virtual ICollection<EmployeeCompanyValue> EmployeeCompanyValues { get; set; } = new List<EmployeeCompanyValue>();
		public ICollection<SystemLog> SystemLogs { get; set; } = new List<SystemLog>();
	}
}
