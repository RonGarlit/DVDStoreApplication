
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // customer
    public partial class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer", "dbo");
            builder.HasKey(x => x.Customerid).HasName("PK__customer__B61ED7F5FB4E6DB9").IsClustered();

            builder.Property(x => x.Customerid).HasColumnName(@"customerid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Storeid).HasColumnName(@"storeid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Firstname).HasColumnName(@"firstname").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45);
            builder.Property(x => x.Lastname).HasColumnName(@"lastname").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45);
            builder.Property(x => x.Email).HasColumnName(@"email").HasColumnType("varchar(50)").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Addressid).HasColumnName(@"addressid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Active).HasColumnName(@"active").HasColumnType("char(1)").IsRequired().IsFixedLength().IsUnicode(false).HasMaxLength(1);
            builder.Property(x => x.Createdate).HasColumnName(@"createdate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Address).WithMany(b => b.Customers).HasForeignKey(c => c.Addressid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkcustomeraddress");
            builder.HasOne(a => a.Store).WithMany(b => b.Customers).HasForeignKey(c => c.Storeid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkcustomerstore");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Customer> builder);
    }

}
// </auto-generated>
