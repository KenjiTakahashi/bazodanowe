using Iesi.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad5.Models {
    class T2 {
        public T2() { t3 = new HashedSet<T3>(); }
        public virtual int id { get; set; }
        public virtual string str { get; set; }
        public virtual ISet<T3> t3 { get; set; }
    }
}
