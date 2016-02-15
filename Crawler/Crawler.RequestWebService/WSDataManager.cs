using BusinessLogic;
using Crawler.Domain.Interfaces;
using Crawler.RequestWebService.WSImplementaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService
{
    public class WSDataManager : IDataManager, IDisposable
    {
        ICustomWebClient webClient;

        IKeywordRepository keyword;
        IPageRepository page;
        IPersonRepository person;
        IPersonPageRankRepository personPageRank;
        ISiteRepository site;
        IDisallowPatternRepository disallowPatterns;

        public WSDataManager(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IKeywordRepository Keywords
        {
            get
            {
                if (keyword == null)
                    keyword = new WSKeywordRepository(webClient);
                return keyword;
            }
        }

        public IPageRepository Pages
        {
            get
            {
                if (page == null)
                    page = new WSPageRepository(webClient);
                return page;
            }
        }

        public IPersonRepository Persons
        {
            get
            {
                if (person == null)
                    person = new WSPersonRepository(webClient);
                return person;
            }
        }

        public IPersonPageRankRepository PersonPageRanks
        {
            get
            {
                if (personPageRank == null)
                    personPageRank = new WSPersonPageRankRepository(webClient);
                return personPageRank;
            }
        }

        public ISiteRepository Sites
        {
            get
            {
                if (site == null)
                    site = new WSSiteRepository(webClient);
                return site;
            }
        }

        public IDisallowPatternRepository DisallowPatterns
        {
            get
            {
                if (disallowPatterns == null)
                    disallowPatterns = new WSDisallowPatternRepository(webClient);
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
