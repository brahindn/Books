using System.ComponentModel.DataAnnotations;


namespace Books.Domain
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
