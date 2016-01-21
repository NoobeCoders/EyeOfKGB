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
    public class KeywordMapper : EntityTypeConfiguration<Keyword>
    {
        public KeywordMapper()
        {
            this.ToTable("Keywords");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.True);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).HasMaxLength(2048);

            this.HasRequired<Person>(c => c.Person)
                .WithMany(c => c.Keywords)
                .HasForeignKey(c => c.PersonId);
        }
    }
}
