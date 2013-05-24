using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    class OutWorkerMap : SubclassMap<OutWorker> {
        public OutWorkerMap() {
            Map(x => x.company).Not.Nullable();
        }
    }
}
