using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class PrzegladMap : EntityTypeConfiguration<Przeglad>
    {
        public PrzegladMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.Zatwierdzony)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Przeglad");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ID_Pojazd).HasColumnName("ID_Pojazd");
            this.Property(t => t.DataPlanowana).HasColumnName("DataPlanowana");
            this.Property(t => t.DataNastepnego).HasColumnName("DataNastepnego");
            this.Property(t => t.Zatwierdzony).HasColumnName("Zatwierdzony");
            this.Property(t => t.ID_Przyjmujacego).HasColumnName("ID_Przyjmujacego");
            this.Property(t => t.ID_Wykonujacego).HasColumnName("ID_Wykonujacego");

            // Relationships
            this.HasRequired(t => t.Pojazd)
                .WithMany(t => t.Przeglads)
                .HasForeignKey(d => d.ID_Pojazd);
            this.HasRequired(t => t.Pracownik)
                .WithMany(t => t.Przeglads)
                .HasForeignKey(d => d.ID_Przyjmujacego);
            this.HasRequired(t => t.Pracownik1)
                .WithMany(t => t.Przeglads1)
                .HasForeignKey(d => d.ID_Wykonujacego);

        }
    }
}
