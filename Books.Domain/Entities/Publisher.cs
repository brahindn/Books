using System.ComponentModel.DataAnnotations;

namespace Books.Domain.Entities
{
    public class Publisher
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
