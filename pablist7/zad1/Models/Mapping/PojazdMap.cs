using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class PojazdMap : EntityTypeConfiguration<Pojazd>
    {
        public PojazdMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.NrRej)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Pojazd");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.id_klient).HasColumnName("id_klient");
            this.Property(t => t.NrRej).HasColumnName("NrRej");
            this.Property(t => t.DataPierwszejRejestracji).HasColumnName("DataPierwszejRejestracji");

            // Relationships
            this.HasRequired(t => t.Klient)
                .WithMany(t => t.Pojazds)
                .HasForeignKey(d => d.id_klient);

        }
    }
}
