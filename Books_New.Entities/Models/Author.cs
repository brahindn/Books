using System.ComponentModel.DataAnnotations;


namespace Books_New.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
