using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    public partial class hid3Context : DbContext {
        static hid3Context() {
            Database.SetInitializer<hid3Context>(null);
        }

        public hid3Context() : base("Name=hid3Context") { }

        public DbSet<Person> People { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<OutWorker> OutWorkers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new WorkerMap());
            modelBuilder.Configurations.Add(new OutWorkerMap());

            modelBuilder.Entity<Worker>().Map(m => m.MapInheritedProperties());
            modelBuilder.Entity<OutWorker>().Map(m => m.MapInheritedProperties());
        }
    }
}
