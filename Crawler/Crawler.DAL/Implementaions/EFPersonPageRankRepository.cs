using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

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
