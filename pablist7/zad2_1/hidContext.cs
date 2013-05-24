using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    public partial class hidContext : DbContext {
        static hidContext() {
            Database.SetInitializer<hidContext>(null);
        }

        public hidContext() : base("Name=hidContext") { }

        public DbSet<Person> People { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<OutWorker> OutWorkers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new WorkerMap());
            modelBuilder.Configurations.Add(new OutWorkerMap());
        }
    }
}
