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
    class EFDisallowPatternRepository : IDisallowPatternRepository
    {
        ApplicationDbContext dbContext;

        public EFDisallowPatternRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(DisallowPattern item)
        {
            dbContext.DisallowPatters.Add(item);
        }

        public void Delete(DisallowPattern item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisallowPattern> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Set(IEnumerable<string> disallowPatterns)
        {
            throw new NotImplementedException();
        }

        public void Update(DisallowPattern item)
        {
            throw new NotImplementedException();
        }

        DisallowPattern IRepository<DisallowPattern>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DisallowPattern> GetBySiteId(int siteId)
        {
            return dbContext.DisallowPatters.Where(d => d.SiteId == siteId);
        }
    }
}
