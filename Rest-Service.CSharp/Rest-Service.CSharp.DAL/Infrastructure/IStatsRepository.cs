using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rest_Service.CSharp.Domain.Entities;

namespace Rest_Service.CSharp.DAL.Infrastructure
{
    public interface IStatsRepository
    {
        IEnumerable<PersonPageRank> GetStatBySite(int id);
        IEnumerable<PersonPageRank> GetStatByRangeDate(int siteId, int personId,DateTime start, DateTime end);
    }
}
