
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // staff
    public partial class StaffMap : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("staff", "dbo");
            builder.HasKey(x => x.Staffid).HasName("PK__staff__645AE4A66EBA5990").IsClustered();

            builder.Property(x => x.Staffid).HasColumnName(@"staffid").HasColumnType("tinyint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Firstname).HasColumnName(@"firstname").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45);
            builder.Property(x => x.Lastname).HasColumnName(@"lastname").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45);
            builder.Property(x => x.Addressid).HasColumnName(@"addressid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Picture).HasColumnName(@"picture").HasColumnType("image(2147483647)").IsRequired(false).HasMaxLength(2147483647);
            builder.Property(x => x.Email).HasColumnName(@"email").HasColumnType("varchar(50)").IsRequired(false).IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Storeid).HasColumnName(@"storeid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Active).HasColumnName(@"active").HasColumnType("bit").IsRequired();
            builder.Property(x => x.Username).HasColumnName(@"username").HasColumnType("varchar(16)").IsRequired().IsUnicode(false).HasMaxLength(16);
            builder.Property(x => x.Password).HasColumnName(@"password").HasColumnType("varchar(40)").IsRequired(false).IsUnicode(false).HasMaxLength(40);
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Address).WithMany(b => b.Staffs).HasForeignKey(c => c.Addressid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkstaffaddress");
            builder.HasOne(a => a.Store).WithMany(b => b.Staffs).HasForeignKey(c => c.Storeid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkstaffstore");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Staff> builder);
    }

}
// </auto-generated>
