
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // salesbyfilmcategory
    public partial class SalesbyfilmcategoryMap : IEntityTypeConfiguration<Salesbyfilmcategory>
    {
        public void Configure(EntityTypeBuilder<Salesbyfilmcategory> builder)
        {
            builder.ToView("salesbyfilmcategory", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.Category).HasColumnName(@"category").HasColumnType("varchar(25)").IsRequired().IsUnicode(false).HasMaxLength(25);
            builder.Property(x => x.Totalsales).HasColumnName(@"totalsales").HasColumnType("decimal(38,2)").HasPrecision(38,2).IsRequired(false);

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Salesbyfilmcategory> builder);
    }

}
// </auto-generated>
