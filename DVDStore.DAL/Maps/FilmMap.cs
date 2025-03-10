
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // film
    public partial class FilmMap : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("film", "dbo");
            builder.HasKey(x => x.Filmid).HasName("PK__film__C037C0C97F935334").IsClustered();

            builder.Property(x => x.Filmid).HasColumnName(@"filmid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Title).HasColumnName(@"title").HasColumnType("varchar(255)").IsRequired().IsUnicode(false).HasMaxLength(255);
            builder.Property(x => x.Description).HasColumnName(@"description").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);
            builder.Property(x => x.Releaseyear).HasColumnName(@"releaseyear").HasColumnType("varchar(4)").IsRequired(false).IsUnicode(false).HasMaxLength(4);
            builder.Property(x => x.Languageid).HasColumnName(@"languageid").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.Originallanguageid).HasColumnName(@"originallanguageid").HasColumnType("tinyint").IsRequired(false);
            builder.Property(x => x.Rentalduration).HasColumnName(@"rentalduration").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.Rentalrate).HasColumnName(@"rentalrate").HasColumnType("decimal(4,2)").HasPrecision(4,2).IsRequired();
            builder.Property(x => x.Length).HasColumnName(@"length").HasColumnType("smallint").IsRequired(false);
            builder.Property(x => x.Replacementcost).HasColumnName(@"replacementcost").HasColumnType("decimal(5,2)").HasPrecision(5,2).IsRequired();
            builder.Property(x => x.Rating).HasColumnName(@"rating").HasColumnType("varchar(10)").IsRequired(false).IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.Specialfeatures).HasColumnName(@"specialfeatures").HasColumnType("varchar(255)").IsRequired(false).IsUnicode(false).HasMaxLength(255);
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Language_Languageid).WithMany(b => b.Films_Languageid).HasForeignKey(c => c.Languageid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkfilmlanguage");
            builder.HasOne(a => a.Originallanguage).WithMany(b => b.Films_Originallanguageid).HasForeignKey(c => c.Originallanguageid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkfilmlanguageoriginal");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Film> builder);
    }

}
// </auto-generated>
