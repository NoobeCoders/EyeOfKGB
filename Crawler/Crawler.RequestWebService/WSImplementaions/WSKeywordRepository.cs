using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSKeywordRepository : IKeywordRepository
    {
        ICustomWebClient webClient;

        public WSKeywordRepository(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public IEnumerable<Keyword> GetKeywordsByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Keyword> GetAll()
        {
            throw new NotImplementedException();
        }

        public Keyword GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Keyword item)
        {
            throw new NotImplementedException();
        }

        public void Update(Keyword item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Keyword item)
        {
            throw new NotImplementedException();
        }
    }
}
