
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // tempActorFilmListing
    public partial class TempActorFilmListingMap : IEntityTypeConfiguration<TempActorFilmListing>
    {
        public void Configure(EntityTypeBuilder<TempActorFilmListing> builder)
        {
            builder.ToTable("tempActorFilmListing", "dbo");
            builder.HasKey(x => new { x.ActorId, x.FirstName, x.LastName, x.FilmId, x.FilmTitle });

            builder.Property(x => x.ActorId).HasColumnName(@"ActorId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.FirstName).HasColumnName(@"FirstName").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45).ValueGeneratedNever();
            builder.Property(x => x.LastName).HasColumnName(@"LastName").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45).ValueGeneratedNever();
            builder.Property(x => x.FilmId).HasColumnName(@"FilmId").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.FilmTitle).HasColumnName(@"FilmTitle").HasColumnType("varchar(255)").IsRequired().IsUnicode(false).HasMaxLength(255).ValueGeneratedNever();
            builder.Property(x => x.Rating).HasColumnName(@"Rating").HasColumnType("varchar(10)").IsRequired(false).IsUnicode(false).HasMaxLength(10);

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<TempActorFilmListing> builder);
    }

}
// </auto-generated>
