using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db {
    public class Admin : User {
        public Admin()
            : base() {
            this.Trusted = true;
        }
    }
}