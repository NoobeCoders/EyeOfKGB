using Crawler.Domain.Interfaces;
using Crawler.RequestWebService.WSImplementaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService
{
    public class DataManager : IDataManager, IDisposable
    {
        IDownloader downloader;

        IKeywordRepository keyword;
        IPageRepository page;
        IPersonRepository person;
        IPersonPageRankRepository personPageRank;
        ISiteRepository site;
        IDisallowPatternRepository disallowPatterns;

        public DataManager (IDownloader downloader)
        {
            downloader = new Downloader();
        }

        public IKeywordRepository Keywords
        {
            get
            {
                if (keyword == null)
                    keyword = new WSKeywordRepository(downloader);
                return keyword;
            }
        }

        public IPageRepository Pages
        {
            get
            {
                if (page == null)
                    page = new WSPageRepository(downloader);
                return page;
            }
        }

        public IPersonRepository Persons
        {
            get
            {
                if (person == null)
                    person = new WSPersonRepository(downloader);
                return person;
            }
        }

        public IPersonPageRankRepository PersonPageRanks
        {
            get
            {
                if (personPageRank == null)
                    personPageRank = new WSPersonPageRankRepository(downloader);
                return personPageRank;
            }
        }

        public ISiteRepository Sites
        {
            get
            {
                if (site == null)
                    site = new WSSiteRepository(downloader);
                return site;
            }
        }

        public IDisallowPatternRepository DisallowPatterns
        {
            get
            {
                if (disallowPatterns == null)
                    disallowPatterns = new WSDisallowPatternRepository(downloader);
                return disallowPatterns;
            }
        }

        public Task Save()
        {            
            throw new NotImplementedException();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    downloader.Dispose();
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
