using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace zad2 {
    public partial class hid2Context : DbContext {
        static hid2Context() {
            Database.SetInitializer<hid2Context>(null);
        }

        public hid2Context() : base("Name=hid2Context") { }

        public DbSet<Person> People { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<OutWorker> OutWorkers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new WorkerMap());
            modelBuilder.Configurations.Add(new OutWorkerMap());

            modelBuilder.Entity<Person>().Map<Worker>(m => m.Requires("PersonType"));
            modelBuilder.Entity<Worker>().Map<OutWorker>(m => m.Requires("PersonType"));
        }
    }
}
