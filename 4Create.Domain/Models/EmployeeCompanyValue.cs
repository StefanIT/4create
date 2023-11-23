using System.ComponentModel.DataAnnotations.Schema;

namespace _4Create.Domain.Models
{
	public class EmployeeCompanyValue : BaseEntity
	{
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }

		[ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
		[ForeignKey(nameof(CompanyId))]
		public Company Company { get; set; }
	}
}
