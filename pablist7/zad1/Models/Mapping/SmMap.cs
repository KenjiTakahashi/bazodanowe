using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class SmMap : EntityTypeConfiguration<Sm>
    {
        public SmMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Sms");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ID_TekstySms).HasColumnName("ID_TekstySms");
            this.Property(t => t.ID_Pojazd).HasColumnName("ID_Pojazd");
            this.Property(t => t.DataWyslania).HasColumnName("DataWyslania");
            this.Property(t => t.DataNastepnego).HasColumnName("DataNastepnego");

            // Relationships
            this.HasRequired(t => t.Pojazd)
                .WithMany(t => t.Sms)
                .HasForeignKey(d => d.ID_Pojazd);
            this.HasRequired(t => t.TekstySm)
                .WithMany(t => t.Sms)
                .HasForeignKey(d => d.ID_TekstySms);

        }
    }
}
