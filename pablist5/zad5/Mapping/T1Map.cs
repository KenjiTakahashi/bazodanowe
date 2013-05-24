using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad5.Models;

namespace zad5.Mapping {
    class T1Map : ClassMap<T1> {
        public T1Map() {
            Id(x => x.id);
            Map(x => x.str);
            HasMany(x => x.t3).KeyColumn("T1_id").Cascade.Delete();
        }
    }
}
