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
    public class SiteMapper : EntityTypeConfiguration<Site>
    {
        public SiteMapper()
        {
            this.ToTable("Sites");

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).HasMaxLength(256);

            this.HasMany<Page>(c => c.Pages)
                .WithRequired(c => c.Site)
                .HasForeignKey(c => c.SiteId)
                .WillCascadeOnDelete();
        }
    }
}
