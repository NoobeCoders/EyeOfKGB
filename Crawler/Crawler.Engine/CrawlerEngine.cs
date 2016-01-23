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
    public class CrawlerEngine
    {
        IDataManager dataManager;
        IDownloader downloader;

        public CrawlerEngine(IDataManager dataManager, IDownloader downloader)
        {
            this.dataManager = dataManager;
            this.downloader = downloader;
        }

        public void Start()
        {
            Parser parser = new Parser();

            IEnumerable<Person> persons = dataManager.Persons.GetAll();
            List<PersonPageRank> personPageRanks = dataManager.PersonPageRanks.GetAll().ToList();

            foreach (Person person in persons)
            {
                Console.WriteLine(person.Name);
            }

            foreach (Site site in dataManager.Sites.GetAll())
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

        private string GetMainURL(string url)
        {
            return url.Split('/').First();
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
    }
}
