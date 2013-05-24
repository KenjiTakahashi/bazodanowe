using Iesi.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace zad1 {
    public class Klient {
        public Klient() { Pojazdy = new HashedSet<Pojazd>(); }
        public virtual int id { get; set; }
        public virtual string pesel { get; set; }
        public virtual string imie { get; set; }
        public virtual string nazwisko { get; set; }
        public virtual string email { get; set; }
        public virtual DateTime datarejestracji { get; set; }
        public virtual string telefon { get; set; }
        public virtual ISet<Pojazd> Pojazdy { get; set; }
    }
}
