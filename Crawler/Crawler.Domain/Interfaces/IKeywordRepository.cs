using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IKeywordRepository : IRepository<Keyword>
    {
        IEnumerable<Keyword> GetKeywordsByName(string name);
    }
}
