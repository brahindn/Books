using System.ComponentModel.DataAnnotations;

namespace Books.Domain
{
    public class Publisher
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
