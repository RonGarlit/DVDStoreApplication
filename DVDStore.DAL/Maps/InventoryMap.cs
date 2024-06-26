
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // inventory
    public partial class InventoryMap : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventory", "dbo");
            builder.HasKey(x => x.Inventoryid).HasName("PK__inventor__C4B4B87AFD12D7F9").IsClustered();

            builder.Property(x => x.Inventoryid).HasColumnName(@"inventoryid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Filmid).HasColumnName(@"filmid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Storeid).HasColumnName(@"storeid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Film).WithMany(b => b.Inventories).HasForeignKey(c => c.Filmid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkinventoryfilm");
            builder.HasOne(a => a.Store).WithMany(b => b.Inventories).HasForeignKey(c => c.Storeid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkinventorystore");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Inventory> builder);
    }

}
// </auto-generated>
