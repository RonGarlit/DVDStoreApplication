
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDStore.DAL
{
    // ****************************************************************************************************
    // DVDStore DAL Code
    // ****************************************************************************************************

    // actor
    public partial class ActorMap : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable("actor", "dbo");
            builder.HasKey(x => x.Actorid).HasName("PK__actor__83335D3387920D2F").IsClustered();

            builder.Property(x => x.Actorid).HasColumnName(@"actorid").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Firstname).HasColumnName(@"firstname").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45);
            builder.Property(x => x.Lastname).HasColumnName(@"lastname").HasColumnType("varchar(45)").IsRequired().IsUnicode(false).HasMaxLength(45);
            builder.Property(x => x.Lastupdate).HasColumnName(@"lastupdate").HasColumnType("datetime").IsRequired();

            InitializePartial(builder);
        }

        partial void InitializePartial(EntityTypeBuilder<Actor> builder);
    }

}
// </auto-generated>
