using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService.WSImplementaions
{
    public class WSPersonPageRankRepository : IPersonPageRankRepository
    {
        ICustomWebClient webClient;

        public WSPersonPageRankRepository(ICustomWebClient webClient)
        {
            this.webClient = webClient;
        }

        public Task<PersonPageRank> GetById(int personId, int pageId)
        {
            throw new NotImplementedException();
        }

        public void Delete(int personId, int pageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PersonPageRank> GetAll()
        {
            throw new NotImplementedException();
        }

        public PersonPageRank GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(PersonPageRank item)
        {
            throw new NotImplementedException();
        }

        public void Update(PersonPageRank item)
        {
            throw new NotImplementedException();
        }

        public void Delete(PersonPageRank item)
        {
            throw new NotImplementedException();
        }
    }
}
