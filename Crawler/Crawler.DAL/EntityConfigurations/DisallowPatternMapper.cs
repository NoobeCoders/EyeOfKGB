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
    class DisallowPatternMapper : EntityTypeConfiguration<DisallowPattern>
    {
        public DisallowPatternMapper()
        {
            this.ToTable("DisallowPatterns");

            this.Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(d => d.Id).IsRequired();

            this.Property(d => d.Pattern).HasMaxLength(2048);
            this.Property(d => d.Agent).HasMaxLength(256);

            this.HasRequired<Site>(d => d.Site)
                .WithMany(s => s.DisallowPatterns)
                .HasForeignKey(d => d.SiteId);

        }
    }
}
