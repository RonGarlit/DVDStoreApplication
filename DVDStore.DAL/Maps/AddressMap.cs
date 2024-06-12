
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // address
    public partial class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address", "dbo");
            builder.HasKey(x => x.Addressid).HasName("PK__address__26A01585FBDE34C9").IsClustered();

            builder.Property(x => x.Addressid).HasColumnName(@"addressid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Address_).HasColumnName(@"address").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Address2).HasColumnName(@"address2").HasColumnType("varchar(50)").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.District).HasColumnName(@"district").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Cityid).HasColumnName(@"cityid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Postalcode).HasColumnName(@"postalcode").HasColumnType("varchar(10)").IsRequired(false).IsUnicode(false).HasMaxLength(10);
            builder.Property(x => x.Phone).HasColumnName(@"phone").HasColumnType("varchar(20)").IsRequired().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.City).WithMany(b => b.Addresses).HasForeignKey(c => c.Cityid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkaddresscity");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Address> builder);
    }

}
// </auto-generated>
