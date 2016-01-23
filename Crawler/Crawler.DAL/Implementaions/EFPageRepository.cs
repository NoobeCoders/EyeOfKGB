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
    public class EFPageRepository : IPageRepository
    {
        ApplicationDbContext dbContext;

        public EFPageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Page> GetPagesByFounDateTime(DateTime date)
        {
            return dbContext.Pages.Where(p => p.FoundDateTime == date);
        }

        public IEnumerable<Page> GetPagesByLastScanDate(DateTime date)
        {
            return dbContext.Pages.Where(p => p.LastScanDate == date);
        }

        public IEnumerable<Page> GetAll()
        {
            return dbContext.Pages;
        }

        public Page GetById(int id)
        {
            return dbContext.Pages.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Page item)
        {
            dbContext.Pages.Add(item);
        }

        public void Update(Page item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(Page item)
        {
            Page page = GetById(item.Id);
            if (page != null)
                dbContext.Pages.Remove(page);
        }
        
        public IEnumerable<Page> GetPagesBySiteId(int id)
        {
            return dbContext.Pages.Where(p => p.SiteId == id);
        }
    }
}
