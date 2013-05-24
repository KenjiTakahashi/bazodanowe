using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    public class Pojazd {
        public virtual int id { get; set; }
        public virtual Klient klient { get; set; }
        public virtual string NrRej { get; set; }
        public virtual DateTime DataPierwszejRejestracji { get; set; }
    }
}
