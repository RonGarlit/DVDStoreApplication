
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // language
    public partial class LanguageMap : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable("language", "dbo");
            builder.HasKey(x => x.Languageid).HasName("PK__language__12686E4A2E3C6EC2").IsClustered();

            builder.Property(x => x.Languageid).HasColumnName(@"languageid").HasColumnType("tinyint").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName(@"name").HasColumnType("char(20)").IsRequired().IsFixedLength().IsUnicode(false).HasMaxLength(20);
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Language> builder);
    }

}
// </auto-generated>
