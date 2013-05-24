using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class UrlopMap : EntityTypeConfiguration<Urlop>
    {
        public UrlopMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Urlop");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.id_pracownik).HasColumnName("id_pracownik");
            this.Property(t => t.data_rozpoczecia).HasColumnName("data_rozpoczecia");
            this.Property(t => t.data_zakonczenia).HasColumnName("data_zakonczenia");

            // Relationships
            this.HasRequired(t => t.Pracownik)
                .WithMany(t => t.Urlops)
                .HasForeignKey(d => d.id_pracownik);

        }
    }
}
