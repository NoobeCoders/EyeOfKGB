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
    public class EFKeywordRepository : IKeywordRepository
    {
        ApplicationDbContext dbContext;

        public EFKeywordRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Keyword> GetKeywordsByName(string name)
        {
            return dbContext.Keywords.Where(x => x.Name == name);
        }

        public IEnumerable<Keyword> GetAll()
        {
            return dbContext.Keywords;
        }

        public Keyword GetById(int id)
        {
            return dbContext.Keywords.Find(id);
        }

        public void Add(Keyword item)
        {
            dbContext.Keywords.Add(item);
        }

        public void Update(Keyword item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Keyword item)
        {
            Keyword keyword = GetById(item.Id);
            if (keyword != null)
                dbContext.Keywords.Remove(keyword);
        }        
    }
}
