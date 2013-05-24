using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class Pracownik
    {
        public Pracownik()
        {
            this.Przeglads = new List<Przeglad>();
            this.Przeglads1 = new List<Przeglad>();
            this.Urlops = new List<Urlop>();
        }

        public int id { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public int rola { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
        public virtual ICollection<Przeglad> Przeglads { get; set; }
        public virtual ICollection<Przeglad> Przeglads1 { get; set; }
        public virtual ICollection<Urlop> Urlops { get; set; }
    }
}
