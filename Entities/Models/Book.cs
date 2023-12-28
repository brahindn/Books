using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public int Pages { get; set; }

        public Guid AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public Guid GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        public Guid PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
