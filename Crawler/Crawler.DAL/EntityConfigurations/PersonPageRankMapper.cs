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
    public class PersonPageRankMapper : EntityTypeConfiguration<PersonPageRank>
    {
        public PersonPageRankMapper()
        {
            this.ToTable("PersonPageRanks");

            this.HasRequired<Person>(c => c.Person)
                .WithMany(c => c.PersonRangeRanks)
                .HasForeignKey(c => c.PersonId);

            this.HasRequired<Page>(c => c.Page)
                .WithMany(c => c.PersonPageRanks)
                .HasForeignKey(c => c.PageId);
        }
    }
}
