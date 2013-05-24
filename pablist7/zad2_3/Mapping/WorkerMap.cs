using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    class WorkerMap : EntityTypeConfiguration<Worker> {
        public WorkerMap() {
            this.Property(t => t.salary);
            this.Property(t => t.position);
        }
    }
}
