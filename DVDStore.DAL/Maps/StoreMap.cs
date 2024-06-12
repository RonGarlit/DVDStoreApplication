
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // store
    public partial class StoreMap : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("store", "dbo");
            builder.HasKey(x => x.Storeid).HasName("PK__store__01A2160BDC00C2A0").IsClustered();

            builder.Property(x => x.Storeid).HasColumnName(@"storeid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Managerstaffid).HasColumnName(@"managerstaffid").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.Addressid).HasColumnName(@"addressid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Address).WithMany(b => b.Stores).HasForeignKey(c => c.Addressid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkstoreaddress");
            builder.HasOne(a => a.Staff).WithMany(b => b.Stores).HasForeignKey(c => c.Managerstaffid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkstorestaff");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Store> builder);
    }

}
// </auto-generated>
