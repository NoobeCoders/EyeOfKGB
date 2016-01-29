using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IDisallowPatternRepository : IRepository<DisallowPattern>
    {
        void Set(IEnumerable<string> disallowPatterns);
    }
}
