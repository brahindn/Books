using System.ComponentModel.DataAnnotations;

namespace Books.Domain.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
