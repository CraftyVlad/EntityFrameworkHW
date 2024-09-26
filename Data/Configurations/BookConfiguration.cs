using BookstoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreApp.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.Author)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Publisher)
                   .HasMaxLength(100);

            builder.Property(b => b.Genre)
                   .HasMaxLength(50);

            builder.Property(b => b.CostPrice)
                   .HasColumnType("decimal(18,2)");

            builder.Property(b => b.SalePrice)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
