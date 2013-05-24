using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class Klient
    {
        public Klient()
        {
            this.Pojazds = new List<Pojazd>();
        }

        public int id { get; set; }
        public string pesel { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string email { get; set; }
        public System.DateTime datarejestracji { get; set; }
        public string telefon { get; set; }
        public virtual ICollection<Pojazd> Pojazds { get; set; }
    }
}
