using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db {
    public class Comment {
        public int ID { get; protected set; }
        [Required] public string Content { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
        public Author Author { get; set; }
    }
}