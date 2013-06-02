using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db {
    public class Shelf {
        public int ID { get; protected set; }
        [Required] public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Shelf() {
            this.Books = new List<Book>();
            this.Users = new List<User>();
        }
    }
}