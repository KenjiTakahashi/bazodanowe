using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class TekstySmMap : EntityTypeConfiguration<TekstySm>
    {
        public TekstySmMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.tresc)
                .IsRequired()
                .HasMaxLength(160);

            // Table & Column Mappings
            this.ToTable("TekstySms");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.tresc).HasColumnName("tresc");
        }
    }
}
