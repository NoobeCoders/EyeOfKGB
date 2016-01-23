using BusinessLogic;
using BusinessLogic.Models;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    public class CrawlerEngine : IDisposable
    {
        IDataManager dataManager;
        IDownloader downloader;
        Parser parser;

        public CrawlerEngine(IDataManager dataManager, IDownloader downloader)
        {
            this.dataManager = dataManager;
            this.downloader = downloader;
            
            parser = new Parser();
        }

        public void Start()
        {
            IEnumerable<Person> persons = dataManager.Persons.GetAll().ToList();
            List<PersonPageRank> personPageRanks = dataManager.PersonPageRanks.GetAll().ToList();

            AddRobotsPageForNewSites();
            ProcessNewPages(persons);

            foreach (Person person in persons)
            {
                Console.WriteLine(person.Name);
            }

            foreach (Site site in dataManager.Sites.GetAll().ToList())
            {
                List<string> pageURLs = new List<string>();

                foreach (Page page in dataManager.Pages.GetPagesBySiteId(site.Id))
                {
                    pageURLs.Add(page.URL);
                }

                string mainURL = GetMainURL(pageURLs[0]);

                string robots = downloader.Download("http://" + mainURL + "/robots.txt");
                string sitemap = downloader.Download("http://" + mainURL + "/sitemap.xml");

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

                    string pageHTML = downloader.Download("http://" + allowPageURL);
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

            dataManager.Save();
        }

        private void AddRobotsPageForNewSites()
        {
            foreach (Site site in dataManager.Sites.GetSitesWithoutPages().ToList())
            {
                site.Pages.Add(new Page()
                                        {
                                            URL = site.Name + "/robots.txt",
                                            LastScanDate = null
                                        });

                dataManager.Sites.Update(site);
            }
        }

        private void ProcessNewPages(IEnumerable<Person> persons)
        {
            IEnumerable<Page> pages = dataManager.Pages.GetPagesByLastScanDate(null).ToList();

            while (pages.Count() != 0)
            {
                foreach (Page page in dataManager.Pages.GetPagesByLastScanDate(null).ToList())
                {
                    ProcessPage(page, persons);
                }

                pages = dataManager.Pages.GetPagesByLastScanDate(null).ToList();
            }
        }

        private void ProcessScannedPages(IEnumerable<Person> persons)
        {
            IEnumerable<Page> pages = dataManager.Pages
                                                    .GetAll()
                                                    .Where(p => p.URL.Contains("sitemap.xml"))
                                                    .Where(p => p.LastScanDate != null)
                                                    .Where(p => p.LastScanDate.Value.Date != DateTime.Now.Date)
                                                    .ToList();

            foreach (Page page in pages)
            {
                ProcessSitemapPage(page);
            }
        }

        private void ProcessPage(Page page, IEnumerable<Person> persons)
        {
            if (isRobotsPage(page))
                ProcessRobotsPage(page);
            else if (isSitemapPage(page))
                ProcessSitemapPage(page);
            else
                ProcessHtmlPage(page, persons);
        }

        private bool isRobotsPage(Page page)
        {
            return page.URL.Contains("robots.txt");
        }

        private bool isSitemapPage(Page page)
        {
            return page.URL.Contains("sitemap.xml");
        }

        private void ProcessRobotsPage(Page page)
        {
            string robots = downloader.Download("http://" + page.URL);

            Site site = page.Site;

            site.Pages.Add(GetSitemapPageFromRobots(robots));

            dataManager.Sites.Update(site);
        }

        private void ProcessSitemapPage(Page page)
        {
            string sitemap = downloader.Download("http://" + page.URL);

            Site site = page.Site;

            AddNewPagesToSiteFromSitemap(site, sitemap);

            dataManager.Sites.Update(site);
        }

        private Page GetSitemapPageFromRobots(string robots)
        {
            string sitemapUrl = parser.GetSitemapUrl(robots);

            return new Page()
                        {
                            URL = sitemapUrl,
                            LastScanDate = null
                        };
        }

        private void AddNewPagesToSiteFromSitemap(Site site, string sitemap)
        {
            IEnumerable<FoundPage> foundPages = parser.GetFoundPages(sitemap);

            List<string> allowPageURLs = new List<string>();

            allowPageURLs = foundPages.Select(p => p.URL).ToList();

            foreach (string allowPageURL in allowPageURLs)
            {
                if (site.Pages.FirstOrDefault(p => p.URL == allowPageURL) == null)
                {
                    site.Pages.Add(new Page()
                    {
                        URL = allowPageURL,
                        Site = site,
                        FoundDateTime = DateTime.Now
                    });
                }
            }
        }

        private void FindNewPagesInSitemap(Site site, string sitemap, IEnumerable<string> disallowPattens)
        {
            IEnumerable<FoundPage> foundPages = parser.GetFoundPages(sitemap);

            List<string> allowPageURLs = new List<string>();

            allowPageURLs = foundPages.Select(p => p.URL).ToList();

            foreach (string disallowPattern in disallowPattens)
            {
                allowPageURLs = allowPageURLs.Where(u => !Regex.IsMatch(u, disallowPattern)).ToList();
            }

            foreach (string allowPageURL in allowPageURLs)
            {
                if (site.Pages.FirstOrDefault(p => p.URL == allowPageURL) == null)
                {
                    site.Pages.Add(new Page()
                    {
                        URL = allowPageURL,
                        Site = site,
                        FoundDateTime = DateTime.Now
                    });
                }
            }
        }

        private void ProcessHtmlPage(Page page, IEnumerable<Person> persons)
        {
            string pageHTML = downloader.Download("http://" + page.URL);
            page.LastScanDate = DateTime.Now;

            IEnumerable<string> pagePhrases = parser.GetPagePhrases(pageHTML);

            foreach (Person person in persons)
            {
                int personPageRankCounter = CountRank(person.Keywords, pagePhrases);

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

            dataManager.Pages.Update(page);
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
