using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Publisher
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
