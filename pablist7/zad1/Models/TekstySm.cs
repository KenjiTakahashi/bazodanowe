using System;
using System.Collections.Generic;

namespace zad1.Models
{
    public partial class TekstySm
    {
        public TekstySm()
        {
            this.Sms = new List<Sm>();
        }

        public int id { get; set; }
        public string tresc { get; set; }
        public virtual ICollection<Sm> Sms { get; set; }
    }
}
