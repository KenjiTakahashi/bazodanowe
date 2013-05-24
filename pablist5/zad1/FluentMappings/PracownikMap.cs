using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    class PracownikMap : ClassMap<Pracownik> {
        public PracownikMap() {
            Id(x => x.id);
            Map(x => x.imie).Not.Nullable();
            Map(x => x.nazwisko).Not.Nullable();
            Map(x => x.rola).Not.Nullable();
            Map(x => x.login).Not.Nullable();
            Map(x => x.haslo).Not.Nullable();
        }
    }
}
