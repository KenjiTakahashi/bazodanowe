using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db {
    public class Book {
        public int ID { get; protected set; }
        [Required] public string Title { get; set; }
        public DateTime Release { get; set; }

        public virtual ICollection<Shelf> Shelves { get; set; }
        [Required] public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Book() {
            this.Shelves = new List<Shelf>();
            this.Authors = new List<Author>();
            this.Comments = new List<Comment>();
        }
    }
}