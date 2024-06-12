
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // filmtext
    public partial class FilmtextMap : IEntityTypeConfiguration<Filmtext>
    {
        public void Configure(EntityTypeBuilder<Filmtext> builder)
        {
            builder.ToTable("filmtext", "dbo");
            builder.HasKey(x => x.Filmid).HasName("PK__filmtext__C037C0C938370F1F").IsClustered();

            builder.Property(x => x.Filmid).HasColumnName(@"filmid").HasColumnType("smallint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Title).HasColumnName(@"title").HasColumnType("varchar(255)").IsRequired().IsUnicode(false).HasMaxLength(255);
            builder.Property(x => x.Description).HasColumnName(@"description").HasColumnType("varchar(max)").IsRequired(false).IsUnicode(false);

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Filmtext> builder);
    }

}
// </auto-generated>
