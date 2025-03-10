
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // rental
    public partial class RentalMap : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rental", "dbo");
            builder.HasKey(x => x.Rentalid).HasName("PK__rental__01677F56951DF45A").IsClustered();

            builder.Property(x => x.Rentalid).HasColumnName(@"rentalid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Rentaldate).HasColumnName(@"rentaldate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Inventoryid).HasColumnName(@"inventoryid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Customerid).HasColumnName(@"customerid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Returndate).HasColumnName(@"returndate").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.Staffid).HasColumnName(@"staffid").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Customer).WithMany(b => b.Rentals).HasForeignKey(c => c.Customerid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkrentalcustomer");
            builder.HasOne(a => a.Inventory).WithMany(b => b.Rentals).HasForeignKey(c => c.Inventoryid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkrentalinventory");
            builder.HasOne(a => a.Staff).WithMany(b => b.Rentals).HasForeignKey(c => c.Staffid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkrentalstaff");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Rental> builder);
    }

}
// </auto-generated>
