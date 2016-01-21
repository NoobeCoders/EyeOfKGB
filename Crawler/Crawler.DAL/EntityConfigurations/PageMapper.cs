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
    public class PageMapper : EntityTypeConfiguration<Page>
    {
        public PageMapper()
        {
            this.ToTable("Pages");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.True);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.URL).HasMaxLength(2048);

            this.HasMany<KeywordMapper>(c => c.Keywords)
                .WithRequired(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .WillCascadeOnDelete;

            this.HasMany<PersonPageRank>(c => c.PersonPageRanks)
                .WithRequired(c => c.Page)
                .HasForeignKey(c => c.PageId)
                .WillCascadeOnDelete;
        }
        
    }
}
