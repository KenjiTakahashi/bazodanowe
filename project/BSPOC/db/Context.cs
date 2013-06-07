using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace db {
    public partial class Context : DbContext {
        static Context() {
            Database.SetInitializer<Context>(null);
        }

        public Context() : base("Name=Context") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}