using System.ComponentModel.DataAnnotations;

namespace _4Create.Domain.Models
{
	public class Title 
	{
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
    }
}
