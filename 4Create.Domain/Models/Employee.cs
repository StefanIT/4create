using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _4Create.Domain.Models
{
	public class Employee : BaseEntity
	{
        [StringLength(50)]
        public required string Email { get; set; }
        public int TitleId { get; set; }
        [ForeignKey(nameof(TitleId))]
        public Title Title { get; set; }

        public virtual ICollection<EmployeeCompanyValue> EmployeeCompanyValues { get; set; } = new List<EmployeeCompanyValue>();
        public ICollection<SystemLog> SystemLogs { get; set; } = new List<SystemLog>();
	}
}
