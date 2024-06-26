
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // logs
    public partial class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("logs", "dbo");
            builder.HasKey(x => x.Logid).HasName("pk_logs").IsClustered();

            builder.Property(x => x.Logid).HasColumnName(@"logid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Level).HasColumnName(@"level").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Callsite).HasColumnName(@"callsite").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Type).HasColumnName(@"type").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Message).HasColumnName(@"message").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Stacktrace).HasColumnName(@"stacktrace").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Innerexception).HasColumnName(@"innerexception").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Additionalinfo).HasColumnName(@"additionalinfo").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            builder.Property(x => x.Loggedondate).HasColumnName(@"loggedondate").HasColumnType("datetime").IsRequired();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Log> builder);
    }

}
// </auto-generated>
