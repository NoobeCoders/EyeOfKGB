using Crawler.DAL.EntityConfigurations;
using Crawler.Domain.Entities;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<PersonPageRank> PersonPageRanks { get; set; }
        public DbSet<DisallowPattern> DisallowPatters { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext()
            : base()
        {

        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KeywordMapper());
            modelBuilder.Configurations.Add(new PageMapper());
            modelBuilder.Configurations.Add(new PersonMapper());
            modelBuilder.Configurations.Add(new PersonPageRankMapper());
            modelBuilder.Configurations.Add(new SiteMapper());
            modelBuilder.Configurations.Add(new DisallowPatternMapper());

            modelBuilder.Configurations.Add(new RoleMapper());
            modelBuilder.Configurations.Add(new UserMapper());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Types().Configure(c =>
            {
                var name = c.ClrType.Name;
                var newName = name.ToLower();
                c.ToTable(newName + "s");
            });

            modelBuilder.Properties().Configure(c =>
            {
                var name = c.ClrPropertyInfo.Name;
                var newName = name.ToLower();
                c.HasColumnName(newName);
            });
        }

    }
}
