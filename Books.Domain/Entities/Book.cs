using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Books.Domain.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public int? Pages { get; set; }

        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public Guid GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        public Guid PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
