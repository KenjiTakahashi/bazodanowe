using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class KlientMap : EntityTypeConfiguration<Klient>
    {
        public KlientMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.pesel)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.imie)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.nazwisko)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.telefon)
                .IsRequired()
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("Klient");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.pesel).HasColumnName("pesel");
            this.Property(t => t.imie).HasColumnName("imie");
            this.Property(t => t.nazwisko).HasColumnName("nazwisko");
            this.Property(t => t.email).HasColumnName("email");
            this.Property(t => t.datarejestracji).HasColumnName("datarejestracji");
            this.Property(t => t.telefon).HasColumnName("telefon");
        }
    }
}
