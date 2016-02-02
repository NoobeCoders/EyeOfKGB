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
        private static readonly int PAGE_INTERVAL = 25;

        IDataManager dataManager;
        IDownloader downloader;
        PageHandler pageHandler;

        IParser parser;

        public CrawlerEngine(IDataManager dataManager, IDownloader downloader)
        {
            this.dataManager = dataManager;
            this.downloader = downloader;

            parser = new Parser("Googlebot");

            pageHandler = new PageHandler(dataManager, downloader, parser);

        }

        public async Task Start()
        {
            await AddRobotsPageForNewSites();
            await ProcessSites(dataManager.Sites.GetAll().Where(s => s.Pages.Count != 0).ToList());
            //await ProcessNewPages();
            //await ProcessScannedSiteMapPages();
            //await ProcessNewPages();
            //await ProcessScannedHtmlPages();
        }

        public void OldStart()
        {
            IEnumerable<Person> persons = dataManager.Persons.GetAll().ToList();
            List<PersonPageRank> personPageRanks = dataManager.PersonPageRanks.GetAll().ToList();

            foreach (Site site in dataManager.Sites.GetAll().ToList())
            {
                List<string> pageURLs = new List<string>();

                foreach (Page page in dataManager.Pages.GetPagesBySiteId(site.Id))
                {
                    pageURLs.Add(page.URL);
                }

                string mainURL = GetMainURL(pageURLs[0]);

                string robots = downloader.Download("http://" + mainURL + "/robots.txt").Result;
                string sitemap = downloader.Download("http://" + mainURL + "/sitemap.xml").Result;

                IEnumerable<string> disallows = parser.GetDisallowPatterns(robots, "Googlebot");
                List<string> allowPageURLs = parser.GetFoundPages(sitemap).ToList();

                foreach (string disallowPattern in disallows)
                {
                    allowPageURLs = allowPageURLs.Where(u => !Regex.IsMatch(u, disallowPattern)).ToList();
                }

                foreach (string allowPageURL in allowPageURLs)
                {
                    if (pageURLs.FirstOrDefault(u => u == allowPageURL) == null)
                    {
                        dataManager.Pages.Add(new Page()
                        {
                            URL = allowPageURL,
                            Site = site,
                            FoundDateTime = DateTime.Now
                        });
                    }
                }

                dataManager.Save();

                foreach (string allowPageURL in allowPageURLs)
                {
                    Page page = dataManager.Pages.GetAll().FirstOrDefault(p => p.URL == allowPageURL);

                    string pageHTML = downloader.Download(allowPageURL).Result;
                    page.LastScanDate = DateTime.Now;

                    IEnumerable<string> pagePhrases = parser.GetPagePhrases(pageHTML);

                    foreach (Person person in persons)
                    {
                        int personPageRankCounter = 0;

                        foreach (Keyword keyword in person.Keywords)
                        {
                            personPageRankCounter += CountPageKeywordUsage(keyword, pagePhrases);
                        }

                        PersonPageRank personPageRank = dataManager.PersonPageRanks.GetById(person.Id, page.Id);

                        if (personPageRank != null)
                        {
                            personPageRank.Rank = personPageRankCounter;
                            dataManager.PersonPageRanks.Update(personPageRank);
                        }
                        else
                        {
                            dataManager.PersonPageRanks.Add(new PersonPageRank()
                            {
                                Person = person,
                                Page = page,
                                Rank = personPageRankCounter
                            });
                        }
                    }
                }
            }
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

                IDataManager dataManager = new DataManager("MSSQLConnection");
                PageHandler pageHandler = new PageHandler(dataManager, downloader, parser);
                siteTasks.Add(ProcessSite(site, dataManager, pageHandler));
                
                if (siteTasks.Count >= SITE_TASK_AMOUNT)
                {
                    await Task.WhenAny(siteTasks);
                }
            }

            await Task.WhenAll(siteTasks);
            try
            { await dataManager.Save(); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            
        }

        private async Task ProcessSite(Site site, IDataManager dataManager, PageHandler pageHandler)
        {
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

        private async Task ProcessNewPages()
        {
            IEnumerable<Page> pages = dataManager.Pages.GetPagesByLastScanDate(null).ToList();

            while (pages.Count() != 0)
            {
                foreach (Page page in pages)
                {
                    await pageHandler.HandlePage(page);
                }

                await dataManager.Save();

                pages = dataManager.Pages.GetPagesByLastScanDate(null).ToList();
            }
        }

        private async Task ProcessScannedSiteMapPages()
        {
            IEnumerable<Page> pages = dataManager.Pages
                                                    .GetAll()
                                                    .Where(p => p.URL.Contains("sitemap.xml"))
                                                    .Where(p => p.LastScanDate != null)
                                                    .Where(p => p.LastScanDate.Value.Date != DateTime.Now.Date)
                                                    .ToList();

            foreach (Page page in pages)
            {
                await pageHandler.HandlePage(page);
            }

            await dataManager.Save();
        }

        private async Task ProcessScannedHtmlPages()
        {
            IEnumerable<Page> pages = dataManager.Pages
                                                    .GetAll()
                                                    .Where(p => !p.URL.Contains("sitemap.xml") && !p.URL.Contains("robots.text"))
                                                    .Where(p => p.LastScanDate != null)
                                                    .Where(p => p.LastScanDate.Value.Date != DateTime.Now.Date)
                                                    .ToList();

            foreach (Page page in pages)
            {
                await pageHandler.HandlePage(page);
            }

            await dataManager.Save();
        }

        private string GetMainURL(string url)
        {
            return url.Split('/').First();
        }

        private int CountRank(IEnumerable<Keyword> keywords, IEnumerable<string> pagePhrases)
        {
            int rank = 0;

            foreach (Keyword keyword in keywords)
            {
                rank += CountPageKeywordUsage(keyword, pagePhrases);
            }

            return rank;
        }

        private int CountPageKeywordUsage(Keyword keyword, IEnumerable<string> pagePhrases)
        {
            int keywordUsage = 0;

            foreach (string phrase in pagePhrases)
            {
                keywordUsage += phrase.Split(' ', ',', '.', ':', ';').Count(s => s == keyword.Name);
            }

            return keywordUsage;
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
