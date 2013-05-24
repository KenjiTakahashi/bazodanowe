using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class Przeglad
    {
        public int id { get; set; }
        public int ID_Pojazd { get; set; }
        public System.DateTime DataPlanowana { get; set; }
        public Nullable<System.DateTime> DataNastepnego { get; set; }
        public byte[] Zatwierdzony { get; set; }
        public int ID_Przyjmujacego { get; set; }
        public int ID_Wykonujacego { get; set; }
        public virtual Pojazd Pojazd { get; set; }
        public virtual Pracownik Pracownik { get; set; }
        public virtual Pracownik Pracownik1 { get; set; }
    }
}
