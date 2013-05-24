using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad5.Models;

namespace zad5.Mapping {
    class T2Map : ClassMap<T2> {
        public T2Map() {
            Id(x => x.id);
            Map(x => x.str);
            HasMany(x => x.t3).KeyColumn("T2_id").Cascade.DeleteOrphan();
        }
    }
}
