using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class Pojazd
    {
        public Pojazd()
        {
            this.Przeglads = new List<Przeglad>();
            this.Sms = new List<Sm>();
        }

        public int id { get; set; }
        public int id_klient { get; set; }
        public string NrRej { get; set; }
        public System.DateTime DataPierwszejRejestracji { get; set; }
        public virtual Klient Klient { get; set; }
        public virtual ICollection<Przeglad> Przeglads { get; set; }
        public virtual ICollection<Sm> Sms { get; set; }
    }
}
