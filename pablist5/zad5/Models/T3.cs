using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad5.Models {
    class T3 {
        public virtual int id { get; set; }
        public virtual string str { get; set; }
        public virtual T1 t1 { get; set; }
        public virtual T2 t2 { get; set; }
    }
}
