using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class Urlop
    {
        public int id { get; set; }
        public int id_pracownik { get; set; }
        public System.DateTime data_rozpoczecia { get; set; }
        public System.DateTime data_zakonczenia { get; set; }
        public virtual Pracownik Pracownik { get; set; }
    }
}
