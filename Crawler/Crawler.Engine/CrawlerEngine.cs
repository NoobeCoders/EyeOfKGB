using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
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
            await ProcessSites();
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
                IEnumerable<FoundPage> foundPages = parser.GetFoundPages(sitemap);

                List<string> allowPageURLs = new List<string>();

                allowPageURLs = foundPages.Select(p => p.URL).ToList();

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

                    string pageHTML = downloader.Download("http://" + allowPageURL).Result;
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
                                            URL = site.Name + "/robots.txt",
                                            FoundDateTime = DateTime.Now,
                                            LastScanDate = null
                                        });

                dataManager.Sites.Update(site);
            }

            var sites = dataManager.Sites.GetAll().Where(s => s.Pages.Count != 0).ToList();

            await dataManager.Save();
        }

        private async Task ProcessSites()
        {
            List<Task> siteTasks = new List<Task>();

            foreach (Site site in dataManager.Sites.GetAll().Where(s => s.Pages.Count != 0).ToList())
            {
                siteTasks.Add(ProcessSite(site));
            }

            await Task.WhenAll(siteTasks);

            await dataManager.Save();
        }

        private async Task ProcessSite(Site site)
        {
            List<Task> pageTasks = new List<Task>();
            int running = 0;

            foreach (Page page in site.Pages.ToList())
            {
                while (running > 50)
                {
                    Task.WaitAny(pageTasks.ToArray());
                    running--;
                }
                pageTasks.Add(pageHandler.HandlePage(page));
                running++;
                Thread.Sleep(50);
            }
            await Task.WhenAll(pageTasks);
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
