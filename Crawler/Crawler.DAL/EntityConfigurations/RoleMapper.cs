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
    public class RoleMapper : EntityTypeConfiguration<Role>
    {
        public RoleMapper()
        {
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.RoleName).HasColumnType("varchar").HasMaxLength(56);

            this.HasMany<User>(c => c.Users)
                .WithRequired(c => c.Role)
                .HasForeignKey(c => c.RoleId)
                .WillCascadeOnDelete();
        }
    }
}
