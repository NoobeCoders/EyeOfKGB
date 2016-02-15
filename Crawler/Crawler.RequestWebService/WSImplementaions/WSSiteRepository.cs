using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSSiteRepository : ISiteRepository
    {
        ICustomWebClient webClient;

        public WSSiteRepository(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public Site GetSiteByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Site> GetSitesWithoutPages()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Site> GetAll()
        {
            throw new NotImplementedException();
        }

        public Site GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Site item)
        {
            throw new NotImplementedException();
        }

        public void Update(Site item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Site item)
        {
            throw new NotImplementedException();
        }
    }
}
