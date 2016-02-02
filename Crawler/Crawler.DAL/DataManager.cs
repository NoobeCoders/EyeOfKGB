﻿using Crawler.DAL.Implementaions;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Crawler.DAL
{
    public class DataManager : IDataManager, IDisposable
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
                    disallowPatterns = new DisallowPatternRepository();
                return disallowPatterns;
            }
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
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