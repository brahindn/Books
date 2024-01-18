using Books_New.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books_New.DataAccess
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title).IsRequired();
            builder.HasIndex(b => new { b.Title, b.AuthorId, b.PublisherId }).IsUnique();
        }
    }
}
