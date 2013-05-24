using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2 {
    public class PersonMap : EntityTypeConfiguration<Person> {
        public PersonMap() {
            this.HasKey(t => t.id);

            this.Property(t => t.name).IsRequired();
            this.Property(t => t.surname).IsRequired();
        }
    }
}
