
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // payment
    public partial class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payment", "dbo");
            builder.HasKey(x => x.Paymentid).HasName("PK__payment__AF26EBEEAC47D667").IsClustered();

            builder.Property(x => x.Paymentid).HasColumnName(@"paymentid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Customerid).HasColumnName(@"customerid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Staffid).HasColumnName(@"staffid").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.Rentalid).HasColumnName(@"rentalid").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Amount).HasColumnName(@"amount").HasColumnType("decimal(5,2)").HasPrecision(5,2).IsRequired();
            builder.Property(x => x.Paymentdate).HasColumnName(@"paymentdate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Customer).WithMany(b => b.Payments).HasForeignKey(c => c.Customerid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkpaymentcustomer");
            builder.HasOne(a => a.Rental).WithMany(b => b.Payments).HasForeignKey(c => c.Rentalid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkpaymentrental");
            builder.HasOne(a => a.Staff).WithMany(b => b.Payments).HasForeignKey(c => c.Staffid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkpaymentstaff");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Payment> builder);
    }

}
// </auto-generated>
