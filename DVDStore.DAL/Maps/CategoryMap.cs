
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // category
    public partial class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category", "dbo");
            builder.HasKey(x => x.Categoryid).HasName("PK__category__23CDE590F71D457F").IsClustered();

            builder.Property(x => x.Categoryid).HasColumnName(@"categoryid").HasColumnType("tinyint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("varchar(25)").IsRequired().IsUnicode(false).HasMaxLength(25);
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Category> builder);
    }

}
// </auto-generated>
