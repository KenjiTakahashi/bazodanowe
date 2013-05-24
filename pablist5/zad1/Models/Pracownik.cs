using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    public class Pracownik {
        public virtual int id { get; set; }
        public virtual string imie { get; set; }
        public virtual string nazwisko { get; set; }
        public virtual int rola { get; set; }
        public virtual string login { get; set; }
        public virtual string haslo { get; set; }
    }
}
