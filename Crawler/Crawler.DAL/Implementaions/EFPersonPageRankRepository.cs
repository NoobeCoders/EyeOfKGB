using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.DAL.Implementaions
{
    public class EFPersonPageRankRepository : IPersonPageRankRepository
    {
        ApplicationDbContext dbContext;

        public EFPersonPageRankRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public PersonPageRank GetById(int personId, int pageId)
        {
            return dbContext.PersonPageRanks.FirstOrDefault(r => r.PersonId == personId && r.PageId == pageId);
        }

        public IEnumerable<PersonPageRank> GetAll()
        {
            return dbContext.PersonPageRanks;
        }

        public PersonPageRank GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(PersonPageRank item)
        {
            dbContext.PersonPageRanks.Add(item);
        }

        public void Update(PersonPageRank item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(PersonPageRank item)
        {
            PersonPageRank rank = GetById(item.PersonId, item.PageId);
            if (rank != null)
                dbContext.PersonPageRanks.Remove(rank);
        }
    }
}
