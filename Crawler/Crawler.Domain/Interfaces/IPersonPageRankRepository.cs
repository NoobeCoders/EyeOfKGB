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
        Task<PersonPageRank> GetById(int personId, int pageId);
    }
}
