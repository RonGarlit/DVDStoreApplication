
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // filmactor
    public partial class FilmactorMap : IEntityTypeConfiguration<Filmactor>
    {
        public void Configure(EntityTypeBuilder<Filmactor> builder)
        {
            builder.ToTable("filmactor", "dbo");
            builder.HasKey(x => new { x.Actorid, x.Filmid }).HasName("PK__filmacto__0F30213F91403294").IsClustered();

            builder.Property(x => x.Actorid).HasColumnName(@"actorid").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Filmid).HasColumnName(@"filmid").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Actor).WithMany(b => b.Filmactors).HasForeignKey(c => c.Actorid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkfilmactoractor");
            builder.HasOne(a => a.Film).WithMany(b => b.Filmactors).HasForeignKey(c => c.Filmid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkfilmactorfilm");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Filmactor> builder);
    }

}
// </auto-generated>
