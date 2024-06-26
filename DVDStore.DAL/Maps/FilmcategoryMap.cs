
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // filmcategory
    public partial class FilmcategoryMap : IEntityTypeConfiguration<Filmcategory>
    {
        public void Configure(EntityTypeBuilder<Filmcategory> builder)
        {
            builder.ToTable("filmcategory", "dbo");
            builder.HasKey(x => new { x.Filmid, x.Categoryid }).HasName("PK__filmcate__D20B1E90ABD8A3C2").IsClustered();

            builder.Property(x => x.Filmid).HasColumnName(@"filmid").HasColumnType("int").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Categoryid).HasColumnName(@"categoryid").HasColumnType("tinyint").IsRequired().ValueGeneratedNever();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Category).WithMany(b => b.Filmcategories).HasForeignKey(c => c.Categoryid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkfilmcategorycategory");
            builder.HasOne(a => a.Film).WithMany(b => b.Filmcategories).HasForeignKey(c => c.Filmid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkfilmcategoryfilm");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Filmcategory> builder);
    }

}
// </auto-generated>
