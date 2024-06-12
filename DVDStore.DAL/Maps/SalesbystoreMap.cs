
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // salesbystore
    public partial class SalesbystoreMap : IEntityTypeConfiguration<Salesbystore>
    {
        public void Configure(EntityTypeBuilder<Salesbystore> builder)
        {
            builder.ToView("salesbystore", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.Storeid).HasColumnName(@"storeid").HasColumnType("int").IsRequired();
            builder.Property(x => x.Store).HasColumnName(@"store").HasColumnType("varchar(101)").IsRequired().IsUnicode(false).HasMaxLength(101);
            builder.Property(x => x.Manager).HasColumnName(@"manager").HasColumnType("varchar(91)").IsRequired().IsUnicode(false).HasMaxLength(91);
            builder.Property(x => x.Totalsales).HasColumnName(@"totalsales").HasColumnType("decimal(38,2)").HasPrecision(38,2).IsRequired(false);

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Salesbystore> builder);
    }

}
// </auto-generated>
