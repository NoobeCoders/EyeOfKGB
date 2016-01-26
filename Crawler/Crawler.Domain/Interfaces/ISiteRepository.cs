using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface ISiteRepository : IRepository<Site>
    {
        Site GetSiteByName(string name);
        IEnumerable<Site> GetSitesWithoutPages();
    }
}
