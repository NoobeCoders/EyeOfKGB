using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Crawler.DAL;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    public class CrawlerEngine : IDisposable
    {
        private static readonly int SITE_TASK_AMOUNT = 20;
        private static readonly int PAGE_TASK_AMOUNT = 50;
        
        private static readonly int PAGE_AMOUNT = 1000;
        private static readonly int PAGE_INTERVAL = 100;

        IDataManager dataManager;
        IDownloader downloader;

        IParser parser;

        public CrawlerEngine(IDataManager dataManager, IDownloader downloader)
        {
            this.dataManager = dataManager;
            this.downloader = downloader;

            parser = new Parser("Googlebot");
        }

        public async Task Start()
        {
            await AddRobotsPageForNewSites();
            await ProcessSites(dataManager.Sites.GetAll().Where(s => s.Pages.Count != 0).ToList());
        }

        private async Task AddRobotsPageForNewSites()
        {
            foreach (Site site in dataManager.Sites.GetSitesWithoutPages().ToList())
            {
                site.Pages.Add(new Page()
                                        {
                                            URL = "http://" + site.Name + "/robots.txt",
                                            FoundDateTime = DateTime.Now,
                                            LastScanDate = null
                                        });

                dataManager.Sites.Update(site);
            }

            await dataManager.Save();
        }

        private async Task ProcessSites(IEnumerable<Site> sites)
        {
            List<Task> siteTasks = new List<Task>();

            foreach (Site site in sites)
            {
                siteTasks.Add(ProcessSite(site));
                
                if (siteTasks.Count >= SITE_TASK_AMOUNT)
                {
                    await Task.WhenAny(siteTasks);
                }
            }

            await Task.WhenAll(siteTasks);
            
        }

        private async Task ProcessSite(Site site)
        {
            using (DataManager dataManager = new DataManager("MSSQLConnection"))
            {
                PageHandler pageHandler = new PageHandler(dataManager, downloader, parser, site);
                List<Task<int>> pageTasks = new List<Task<int>>();

                IEnumerable<Page> pages = await dataManager.Pages.GetPagesBySiteId(site.Id, PAGE_AMOUNT);

                foreach (Page page in pages)
                {
                    pageTasks.Add(pageHandler.HandlePage(page));

                    Thread.Sleep(PAGE_INTERVAL);

                    if (pageTasks.Count >= PAGE_TASK_AMOUNT)
                    {
                        Task<int> finishedTask = await Task.WhenAny(pageTasks);
                        pageTasks.Remove(finishedTask);
                    }
                }
                await Task.WhenAll(pageTasks);

                await dataManager.Save();
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dataManager.Dispose();
                    downloader.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
