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
    public class WSKeywordRepository : IKeywordRepository
    {
        ICustomWebClient webClient;

        public WSKeywordRepository(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IEnumerable<Keyword> GetAll()
        {
            string answer = webClient.GetRequest("/api/keywords/GetKeywords/");
            return Json.Decode<List<Keyword>>(answer);
        }

        public IEnumerable<Keyword> GetKeywordsByName(string name)
        {
            IEnumerable<Keyword> keywords = GetAll();
            return keywords.Where(k => k.Name == name);
        }

        public Keyword GetById(int id)
        {
            IEnumerable<Keyword> keywords = GetAll();
            return keywords.FirstOrDefault(k => k.Id == id);
        }

        public void Add(Keyword keyword)
        {
            webClient.PostRequest("/api/keywords/PostKeyword/", Json.Encode(keyword));
        }

        public void Update(Keyword keyword)
        {
            webClient.PutRequest("/api/keywords/PutKeyword/", Json.Encode(keyword));
        }

        public void Delete(int id)
        {
            webClient.DeleteRequest("/api/keywords/DeleteKeyword/", Json.Encode(id));
        }

        public void Delete(Keyword keyword)
        {
            Delete(keyword.Id);
        }
    }
}
