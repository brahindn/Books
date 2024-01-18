using System.ComponentModel.DataAnnotations;

namespace Books_New.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
