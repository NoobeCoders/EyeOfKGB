using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.PageParser;
using Crawler.DAL;
using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            DataManager dataManager = new DataManager();
            Downloader downLoader = new Downloader();
            Parser parser = new Parser();

            IEnumerable<Person> persons = context.Persons; //dataManager.Persons.GetAll();
            List<PersonPageRank> personPageRanks = context.PersonPageRanks.ToList(); // dataManager.PersonPageRanks.GetAll().ToList();

            foreach (Person person in persons)
            {
                Console.WriteLine(person.Name);
            }

            foreach (Site site in dataManager.Sites.GetAll())
            {
                List<string> pageURLs = new List<string>();

                foreach (Page page in context.Pages.Where(p => p.SiteId == site.Id))//dataManager.Pages.GetPagesBySiteId(site.Id))
                {
                    pageURLs.Add(page.URL);
                }

                string mainURL = GetMainURL(pageURLs[0]);

                string robots = downLoader.Download(mainURL + "/robots.txt");
                string sitemap = downLoader.Download(mainURL + "/sitemap.xml");

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

                    string pageHTML = downLoader.Download("http://" + allowPageURL);

                    IEnumerable<string> pagePhrases = parser.GetPagePhrases(pageHTML);

                    foreach (Person person in persons)
                    {
                        int personPageRankCounter = 0;

                        foreach (Keyword keyword in person.Keywords)
                        {
                            personPageRankCounter += CountPageKeywordUsage(keyword, pagePhrases);
                        }

                        PersonPageRank personPageRank = personPageRanks.FirstOrDefault(r => r.PersonId == person.Id && r.Page.Id == page.Id);

                        if (personPageRank != null)
                        {
                            personPageRank.Rank = personPageRankCounter;
                        }
                        else
                        {
                            personPageRanks.Add(new PersonPageRank()
                            {
                                Person = person,
                                Page = page
                            });
                        }
                    }
                }

                dataManager.Save();
            }




            //Downloader dawnloader = new Downloader();
            //string text = dawnloader.Download(@"http://oper.ru/");

            //Person person = new Person();
            //person.Keywords = new List<Keyword>();
            //person.Keywords.Add(new Keyword() { Name = "Путин" });
            //person.Keywords.Add(new Keyword() { Name = "Путином" });
            //person.Keywords.Add(new Keyword() { Name = "Путина" });
            //foreach (string url in PageLinkFinder.FindPageUrls(text))
            //{
            //    Console.WriteLine(url);
            //}
            //PersonRanker ranker = new PersonRanker(person);
            //Console.WriteLine(ranker.GetPersonPageRank(text));
            Console.ReadKey();

            context.Dispose();
        }

        static string GetMainURL(string url)
        {
            return url.Split('/').First();
        }

        static int CountPageKeywordUsage(Keyword keyword, IEnumerable<string> pagePhrases)
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
