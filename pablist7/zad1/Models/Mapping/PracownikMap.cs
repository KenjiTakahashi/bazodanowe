using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad1.Models.Mapping
{
    public class PracownikMap : EntityTypeConfiguration<Pracownik>
    {
        public PracownikMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.imie)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.nazwisko)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.login)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.haslo)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Pracownik");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.imie).HasColumnName("imie");
            this.Property(t => t.nazwisko).HasColumnName("nazwisko");
            this.Property(t => t.rola).HasColumnName("rola");
            this.Property(t => t.login).HasColumnName("login");
            this.Property(t => t.haslo).HasColumnName("haslo");
        }
    }
}
