using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    public class Przeglad {
        public virtual int id { get; set; }
        public virtual Pojazd Pojazd { get; set; }
        public virtual DateTime DataPlanowana { get; set; }
        public virtual DateTime DataNastepnego { get; set; }
        public virtual Pracownik Przyjmujacy { get; set; }
        public virtual Pracownik Wykonujacy { get; set; }
    }
}
