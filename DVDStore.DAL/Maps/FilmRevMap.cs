
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // filmRev
    public partial class FilmRevMap : IEntityTypeConfiguration<FilmRev>
    {
        public void Configure(EntityTypeBuilder<FilmRev> builder)
        {
            builder.ToTable("filmRev", "dbo");
            builder.HasKey(x => new { x.Filmid, x.Title, x.Rentalduration, x.Rentalrate, x.Replacementcost, x.Categoryid, x.Name });

            builder.Property(x => x.Filmid).HasColumnName(@"filmid").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Title).HasColumnName(@"title").HasColumnType("varchar(255)").IsRequired().IsUnicode(false).HasMaxLength(255).ValueGeneratedNever();
            builder.Property(x => x.Originallanguageid).HasColumnName(@"originallanguageid").HasColumnType("tinyint").IsRequired(false);
            builder.Property(x => x.Rentalduration).HasColumnName(@"rentalduration").HasColumnType("tinyint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Rentalrate).HasColumnName(@"rentalrate").HasColumnType("decimal(4,2)").HasPrecision(4,2).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Length).HasColumnName(@"length").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.Replacementcost).HasColumnName(@"replacementcost").HasColumnType("decimal(5,2)").HasPrecision(5,2).IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Rating).HasColumnName(@"rating").HasColumnType("varchar(10)").IsRequired(false).IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.Specialfeatures).HasColumnName(@"specialfeatures").HasColumnType("varchar(255)").IsRequired(false).IsUnicode(false).HasMaxLength(255);
            builder.Property(x => x.Categoryid).HasColumnName(@"categoryid").HasColumnType("tinyint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("varchar(25)").IsRequired().IsUnicode(false).HasMaxLength(25).ValueGeneratedNever();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<FilmRev> builder);
    }

}
// </auto-generated>
