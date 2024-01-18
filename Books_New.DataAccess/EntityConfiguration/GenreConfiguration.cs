using Books_New.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books_New.DataAccess
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(g => g.Name).IsRequired();
            builder.HasIndex(g => g.Name).IsUnique();
        }
    }
}
