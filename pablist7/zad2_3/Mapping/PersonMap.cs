using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace zad2 {
    public class PersonMap : EntityTypeConfiguration<Person> {
        public PersonMap() {
            this.HasKey(t => t.id);

            this.Property(t => t.name).IsRequired().HasMaxLength(50);
            this.Property(t => t.surname).IsRequired().HasMaxLength(50);
        }
    }
}
