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
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();

            this.Property(c => c.Name).HasMaxLength(56).IsRequired();
            this.Property(c => c.Login).HasMaxLength(56).IsRequired();
            this.Property(c => c.Password).HasMaxLength(56).IsRequired();
            this.Property(c => c.Firstname).HasMaxLength(56).IsRequired();

            this.HasRequired<Role>(c => c.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.RoleId);
        }
    }
}
