using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    class OutWorkerMap : EntityTypeConfiguration<OutWorker> {
        public OutWorkerMap() {
            this.HasKey(t => t.id);

            this.Property(t => t.name).IsRequired();
            this.Property(t => t.surname).IsRequired();
            this.Property(t => t.salary).IsRequired();
            this.Property(t => t.position).IsRequired();
            this.Property(t => t.company).IsRequired();
        }
    }
}
