using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    class WorkerMap : SubclassMap<Worker> {
        public WorkerMap() {
            Map(x => x.salary).Not.Nullable();
            Map(x => x.position).Not.Nullable();
        }
    }
}
