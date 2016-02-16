using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSSiteRepository : ISiteRepository
    {
        ICustomWebClient webClient;

        public WSSiteRepository(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IEnumerable<Site> GetAll()
        {
            string answer = webClient.GetRequest("api/v1/sites/");
            return Json.Decode<List<Site>>(answer);
        }

        public Site GetSiteByName(string name)
        {
            IEnumerable<Site> sites = GetAll();
            return sites.FirstOrDefault(s => s.Name == name);
        }

        public Site GetById(int id)
        {
            IEnumerable<Site> sites = GetAll();
            return sites.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Site> GetSitesWithoutPages()
        {
            throw new NotImplementedException();
        }
       
        public void Add(Site site)
        {
            webClient.PostRequest("api/v1/sites/", Json.Encode(site));
        }

        public void Update(Site site)
        {
            webClient.PutRequest("api/v1/sites/", Json.Encode(site));
        }

        public void Delete(int id)
        {
            webClient.DeleteRequest("api/v1/sites/", Json.Encode(id));
        }

        public void Delete(Site site)
        {
            Delete(site.Id);
        }
    }
}
