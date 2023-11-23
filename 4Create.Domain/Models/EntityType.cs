using System.ComponentModel.DataAnnotations;

namespace _4Create.Domain.Models
{
	public class EntityType 
	{
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
    }
}
