using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad5.Models;

namespace zad5.Mapping {
    class T3Map : ClassMap<T3> {
        public T3Map() {
            Id(x => x.id);
            Map(x => x.str);
            References(x => x.t1);
            References(x => x.t2);
        }
    }
}
