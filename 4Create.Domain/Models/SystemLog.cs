using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _4Create.Domain.Models
{
	public class SystemLog : BaseEntity
	{
        public string ResourceType { get; set; }
        public int EntityTypeId { get; set; }
        [StringLength(50)]
        public string Event { get; set; }
        public string ResourceAttributes { get; set; }
        public string Comment { get; set; }
    }
}
