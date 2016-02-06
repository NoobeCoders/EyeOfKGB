using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IPersonPageRankRepository : IRepository<PersonPageRank>
    {
        PersonPageRank GetById(int personId, int pageId);

        void Delete(int personId, int pageId);
    }
}
