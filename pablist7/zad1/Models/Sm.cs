using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class Sm
    {
        public int id { get; set; }
        public int ID_TekstySms { get; set; }
        public int ID_Pojazd { get; set; }
        public Nullable<System.DateTime> DataWyslania { get; set; }
        public System.DateTime DataNastepnego { get; set; }
        public virtual Pojazd Pojazd { get; set; }
        public virtual TekstySm TekstySm { get; set; }
    }
}
