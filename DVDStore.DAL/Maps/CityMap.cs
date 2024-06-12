
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // city
    public partial class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("city", "dbo");
            builder.HasKey(x => x.Cityid).HasName("PK__city__B4BDBD26C7DB32B9").IsClustered();

            builder.Property(x => x.Cityid).HasColumnName(@"cityid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.City_).HasColumnName(@"city").HasColumnType("varchar(50)").IsRequired().IsUnicode(false).HasMaxLength(50);
            builder.Property(x => x.Countryid).HasColumnName(@"countryid").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            builder.HasOne(a => a.Country).WithMany(b => b.Cities).HasForeignKey(c => c.Countryid).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fkcitycountry");

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<City> builder);
    }

}
// </auto-generated>
