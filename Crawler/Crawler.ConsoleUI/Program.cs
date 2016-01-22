using BusinessLogic;
using BusinessLogic.Models;
using BusinessLogic.PageParser;
using Crawler.DAL;
using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager dataManager = new DataManager();
            Downloader downLoader = new Downloader();
            Parser parser = new Parser();

            foreach (Site site in dataManager.Sites.GetAll())
            {
                List<string> pageURLs = new List<string>();

                foreach(Page page in dataManager.Pages.GetPagesBySiteId(site.Id))
                {
                    pageURLs.Add(page.URL);
                }

                string mainURL = GetMainURL(pageURLs[0]);

                string robots = downLoader.Download(mainURL + "/robots.txt");
                string sitemap = downLoader.Download(mainURL + "/sitemap.xml");

                IEnumerable<string> disallows = parser.GetDisallowPatterns(robots, "Googlebot");
                IEnumerable<FoundPage> pages = parser.GetFoundPages(sitemap);


                pages.Where(x => !Regex.IsMatch(url, disallowPattern))

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
            //Console.ReadKey();
        }

        static string GetMainURL(string url)
        {
            return url.Split('/').First();
        }
    }
}
