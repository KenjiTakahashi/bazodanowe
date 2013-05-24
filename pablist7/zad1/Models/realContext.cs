using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using zad1.Models.Mapping;

namespace zad1.Models
{
    public partial class realContext : DbContext
    {
        static realContext()
        {
            Database.SetInitializer<realContext>(null);
        }

        public realContext()
            : base("Name=realContext")
        {
        }

        public DbSet<Klient> Klients { get; set; }
        public DbSet<Pojazd> Pojazds { get; set; }
        public DbSet<Pracownik> Pracowniks { get; set; }
        public DbSet<Przeglad> Przeglads { get; set; }
        public DbSet<Sm> Sms { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TekstySm> TekstySms { get; set; }
        public DbSet<Urlop> Urlops { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KlientMap());
            modelBuilder.Configurations.Add(new PojazdMap());
            modelBuilder.Configurations.Add(new PracownikMap());
            modelBuilder.Configurations.Add(new PrzegladMap());
            modelBuilder.Configurations.Add(new SmMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TekstySmMap());
            modelBuilder.Configurations.Add(new UrlopMap());
        }
    }
}
