using Crawler.DAL.EntityConfigurations;
using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<PersonPageRank> PersonPageRanks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KeywordMapper());
            modelBuilder.Configurations.Add(new PageMapper());
            modelBuilder.Configurations.Add(new PersonMapper());
            modelBuilder.Configurations.Add(new PersonPageRankMapper());
            modelBuilder.Configurations.Add(new SiteMapper());

            base.OnModelCreating(modelBuilder);
        }

    }
}
