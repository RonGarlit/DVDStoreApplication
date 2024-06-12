
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // stafflist
    public partial class StafflistMap : IEntityTypeConfiguration<Stafflist>
    {
        public void Configure(EntityTypeBuilder<Stafflist> builder)
        {
            builder.ToView("stafflist", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.Id).HasColumnName(@"ID").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("varchar(91)").IsRequired().IsUnicode(false).HasMaxLength(91);
            builder.Property(x => x.Address).HasColumnName(@"address").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Zipcode).HasColumnName(@"zipcode").HasColumnType("varchar(10)").IsRequired(false).IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.Phone).HasColumnName(@"phone").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.City).HasColumnName(@"city").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Country).HasColumnName(@"country").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Sid).HasColumnName(@"SID").HasColumnType("int").IsRequired();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Stafflist> builder);
    }

}
// </auto-generated>
