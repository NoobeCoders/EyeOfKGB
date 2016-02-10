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

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.URL).HasMaxLength(2048);

            this.HasRequired<Site>(c => c.Site)
                .WithMany(c => c.Pages)
                .HasForeignKey(c => c.SiteId);

            this.HasMany<PersonPageRank>(c => c.PersonPageRanks)
                .WithRequired(c => c.Page)
                .HasForeignKey(c => c.PageId)
                .WillCascadeOnDelete();
        }
        
    }
}
