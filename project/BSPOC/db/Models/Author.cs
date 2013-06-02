using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db {
    public class Author {
        public int ID { get; protected set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Author() {
            this.Books = new List<Book>();
            this.Comments = new List<Comment>();
        }
    }
}