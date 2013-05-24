using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    class PojazdMap : ClassMap<Pojazd> {
        public PojazdMap() {
            Id(x => x.id);
            References(x => x.klient).Column("id_klient");
            Map(x => x.NrRej).Not.Nullable();
            Map(x => x.DataPierwszejRejestracji).Not.Nullable();
        }
    }
}
