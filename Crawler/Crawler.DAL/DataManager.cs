using DAL.Implementaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL
{
    public class DataManager : IDisposable
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        EFKeywordRepository keyword;
        EFPageRepository page;
        EFPersonRepository person;
        EFPersonPageRankRepository personPageRank;
        EFSiteRepository site;

        public EFKeywordRepository Keywords
        {
            get
            {
                if (keyword == null)
                    keyword = new EFKeywordRepository(dbContext);
                return keyword;
            }
        }

        public EFPageRepository Pages
        {
            get
            {
                if (page == null)
                    page = new EFPageRepository(dbContext);
                return page;
            }
        }

        public EFPersonRepository Persons
        {
            get
            {
                if (person == null)
                    person = new EFPersonRepository(dbContext);
                return person;
            }
        }

        public EFPersonPageRankRepository PersonPageRanks
        {
            get
            {
                if (personPageRank == null)
                    personPageRank = new EFPersonPageRankRepository(dbContext);
                return personPageRank;
            }
        }

        public EFSiteRepository Sites
        {
            get
            {
                if (site == null)
                    site = new EFSiteRepository(dbContext);
                return site;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
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
