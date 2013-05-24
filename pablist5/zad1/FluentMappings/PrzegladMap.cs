using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    class PrzegladMap : ClassMap<Przeglad> {
        public PrzegladMap() {
            Id(x => x.id);
            HasOne(x => x.Pojazd);
            Map(x => x.DataPlanowana).Not.Nullable();
            Map(x => x.DataNastepnego);
            HasOne(x => x.Przyjmujacy);
            HasOne(x => x.Wykonujacy);
        }
    }
}
