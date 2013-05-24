using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    class KlientMap : ClassMap<Klient> {
        public KlientMap() {
            Id(x => x.id);
            Map(x => x.pesel).Not.Nullable();
            Map(x => x.imie).Not.Nullable();
            Map(x => x.nazwisko).Not.Nullable();
            Map(x => x.email).Not.Nullable();
            Map(x => x.datarejestracji).Not.Nullable();
            Map(x => x.telefon).Not.Nullable();
            HasMany(x => x.Pojazdy).KeyColumn("id_klient").Cascade.DeleteOrphan();
        }
    }
}
