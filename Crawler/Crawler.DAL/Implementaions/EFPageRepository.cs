using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
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

        public IEnumerable<Page> GetPagesBySiteId(int id)
        {
            return dbContext.Pages.Where(p => p.SiteId == id);
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
            Delete(item.Id);
        }

        public void Delete(int id)
        {
            Page page = GetById(id);
            if (page != null)
                dbContext.Pages.Remove(page);
        }

        public async Task<IEnumerable<Page>> GetPagesBySiteId(int id, int pageAmount)
        {
            IEnumerable<Page> pages = null;

            bool success = false;

            do
            {
                try
                {
                    using (DbContextTransaction transaction = dbContext.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                    {
                        pages = await dbContext.Pages.Where(p => p.SiteId == id)
                        .Where(p => (p.LastScanDate != null && DbFunctions.DiffDays(p.LastScanDate, DateTime.Now) != 0) || p.LastScanDate == null)
                        .Where(p => p.LastProcessDate == null || (p.LastProcessDate != null
                                                                    && ((p.LastScanDate == null
                                                                            && DbFunctions.DiffHours(p.LastProcessDate, DateTime.Now) > 3)
                                                                        || (p.LastScanDate != null
                                                                            && (DbFunctions.DiffMicroseconds(p.LastScanDate, p.LastProcessDate) > 0
                                                                                    || DbFunctions.DiffHours(p.LastProcessDate, DateTime.Now) > 3
                                                                                )
                                                                            )
                                                                        )
                                                                    ))
                        .Take(pageAmount).ToListAsync<Page>();

                        foreach (Page page in pages)
                        {
                            page.LastProcessDate = DateTime.Now;
                        }


                        await dbContext.SaveChangesAsync();

                        transaction.Commit();

                        success = true;
                    }
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!success);

            return pages;
        }

        public bool IsNewUrl(string url)
        {
            try
            {
                if (dbContext.Pages.FirstOrDefault(p => p.URL == url) == null)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> IsNewUrlAsync(string url)
        {
            if (await dbContext.Pages.FirstOrDefaultAsync(p => p.URL == url) == null)
                return true;
            else
                return false;
        }

        public async Task<Page> GetByIdAsync(int pageId)
        {
            return await dbContext.Pages.FindAsync(pageId);
        }
    }
}
