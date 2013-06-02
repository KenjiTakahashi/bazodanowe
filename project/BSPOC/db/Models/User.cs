using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db {
    public class User {
        public int ID { get; protected set; }
        [Required] public string Name { get; set; }
        [Required] public string Surname { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Nickname { get; set; }
        [Required] public string Password { get; set; }

        public virtual ICollection<Shelf> Shelves { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        [Required] public bool Trusted { get; set; }

        public User() {
            this.Shelves = new List<Shelf>();
            this.Comments = new List<Comment>();
        }
    }
}