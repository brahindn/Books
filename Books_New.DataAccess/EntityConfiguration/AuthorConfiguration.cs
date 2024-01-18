using Books_New.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books_New.DataAccess
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name).IsRequired();
            builder.HasIndex(a => a.Name).IsUnique();
        }
    }
}
