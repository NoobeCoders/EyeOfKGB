using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IDataManager
    {
        IPersonRepository Persons { get; }
        IKeywordRepository Keywords { get; }
        ISiteRepository Sites { get; }
        IPersonPageRankRepository PersonPageRanks { get; }
        IPageRepository Pages { get; }
        IDisallowPatternRepository DisallowPatterns { get; }

        Task Save();
        void Dispose();
    }
}
