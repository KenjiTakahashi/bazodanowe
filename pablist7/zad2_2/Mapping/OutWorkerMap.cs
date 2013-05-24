using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    class OutWorkerMap : EntityTypeConfiguration<OutWorker> {
        public OutWorkerMap() {
            this.Property(t => t.company);
        }
    }
}
