using System.ComponentModel.DataAnnotations;

namespace Books.Domain
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
