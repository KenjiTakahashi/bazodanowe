using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    class WorkerMap : ClassMap<Worker> {
        public WorkerMap() {
            Id(x => x.id);
            Map(x => x.name).Not.Nullable();
            Map(x => x.surname).Not.Nullable();
            Map(x => x.salary).Not.Nullable();
            Map(x => x.position).Not.Nullable();
        }
    }
}
