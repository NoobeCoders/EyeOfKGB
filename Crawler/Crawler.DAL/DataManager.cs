using Crawler.DAL.Implementaions;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Crawler.DAL
{
    public class DataManager : IDataManager
    {
        ApplicationDbContext dbContext;

        IKeywordRepository keyword;
        IPageRepository page;
        IPersonRepository person;
        IPersonPageRankRepository personPageRank;
        ISiteRepository site;
        IDisallowPatternRepository disallowPatterns;

        public DataManager (string connectionString)
        {
            dbContext = new ApplicationDbContext(connectionString);
        }

        public IKeywordRepository Keywords
        {
            get
            {
                if (keyword == null)
                    keyword = new EFKeywordRepository(dbContext);
                return keyword;
            }
        }

        public IPageRepository Pages
        {
            get
            {
                if (page == null)
                    page = new EFPageRepository(dbContext);
                return page;
            }
        }

        public IPersonRepository Persons
        {
            get
            {
                if (person == null)
                    person = new EFPersonRepository(dbContext);
                return person;
            }
        }

        public IPersonPageRankRepository PersonPageRanks
        {
            get
            {
                if (personPageRank == null)
                    personPageRank = new EFPersonPageRankRepository(dbContext);
                return personPageRank;
            }
        }

        public ISiteRepository Sites
        {
            get
            {
                if (site == null)
                    site = new EFSiteRepository(dbContext);
                return site;
            }
        }

        public IDisallowPatternRepository DisallowPatterns
        {
            get
            {
                if (disallowPatterns == null)
                    disallowPatterns = new DisallowPatternRepository(dbContext);
                return disallowPatterns;
            }
        }

        public async Task Save()
        {
            bool success = false;

            do
            {
                try
                {
                    await dbContext.SaveChangesAsync();

                    success = true;
                }
                catch (DbUpdateException ex)
                {
                    foreach (DbEntityEntry entry in ex.Entries.Where(x => x.State == EntityState.Modified))
                    {
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                    }

                    foreach (DbEntityEntry entry in ex.Entries.Where(x => x.State == EntityState.Added))
                    {
                        entry.State = EntityState.Detached;
                    }

                    foreach (DbEntityEntry entry in ex.Entries.Where(x => x.State == EntityState.Deleted))
                    {
                        entry.State = EntityState.Unchanged;
                    }
                }

            } while (!success);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
