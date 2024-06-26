
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // filmlist
    public partial class FilmlistMap : IEntityTypeConfiguration<Filmlist>
    {
        public void Configure(EntityTypeBuilder<Filmlist> builder)
        {
            builder.ToView("filmlist", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.Fid).HasColumnName(@"FID").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Title).HasColumnName(@"title").HasColumnType("varchar(255)").IsRequired(false).IsUnicode(false).HasMaxLength(255);
            builder.Property(x => x.Description).HasColumnName(@"description").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);
            builder.Property(x => x.Category).HasColumnName(@"category").HasColumnType("varchar(25)").IsRequired().IsUnicode(false).HasMaxLength(25);
            builder.Property(x => x.Price).HasColumnName(@"price").HasColumnType("decimal(4,2)").HasPrecision(4,2).IsRequired(false);
            builder.Property(x => x.Length).HasColumnName(@"length").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.Rating).HasColumnName(@"rating").HasColumnType("varchar(10)").IsRequired(false).IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.Actors).HasColumnName(@"actors").HasColumnType("varchar(91)").IsRequired().IsUnicode(false).HasMaxLength(91);

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Filmlist> builder);
    }

}
// </auto-generated>
