using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public IEnumerable<Page> GetPagesByLastScanDate(DateTime? date)
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

        public async Task<IEnumerable<Page>> GetPagesBySiteId(int id, int pageAmount)
        {
            IEnumerable<Page> pages;

            using (DbContextTransaction transaction = dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                bool success = false;
                
                do
                {
                    pages = await dbContext.Pages
                        .Where(p => (p.LastScanDate != null && DbFunctions.DiffDays(p.LastScanDate, DateTime.Now) != 0) || p.LastScanDate == null)
                        .Where(p => p.LastProcessDate == null || (p.LastProcessDate != null 
                                                                    && ((p.LastScanDate == null 
                                                                            && DbFunctions.DiffHours(DateTime.Now, p.LastProcessDate) > 3) 
                                                                        || (p.LastScanDate != null 
                                                                            && (DbFunctions.DiffMicroseconds(p.LastScanDate, p.LastProcessDate) > 0
                                                                                    || DbFunctions.DiffHours(DateTime.Now, p.LastProcessDate) > 3
                                                                                )
                                                                            )
                                                                        )
                                                                  ))
                        .Take(pageAmount).ToListAsync<Page>();

                    foreach (Page page in pages)
                    {
                        page.LastProcessDate = DateTime.Now;
                    }

                    try
                    {
                        await dbContext.SaveChangesAsync();

                        transaction.Commit();

                        success = true;
                    }
                    catch (DbUpdateException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!success);
            }

            return pages;
        }
    }
}
