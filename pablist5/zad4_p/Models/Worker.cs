﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    public class Worker : Person {
        public virtual int salary { get; set; }
        public virtual string position { get; set; }
    }
}
