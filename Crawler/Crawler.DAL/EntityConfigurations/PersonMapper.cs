using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.EntityConfigurations
{
    public class PersonMapper : EntityTypeConfiguration<Person>
    {
        public PersonMapper()
        {
            this.ToTable("Persons");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).HasMaxLength(2048).IsUnicode(true);

            this.HasMany<Keyword>(c => c.Keywords)
                .WithRequired(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .WillCascadeOnDelete();
        }
    }
}
