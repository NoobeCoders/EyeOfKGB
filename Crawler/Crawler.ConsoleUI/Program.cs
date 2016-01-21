using BusinessLogic;
using BusinessLogic.PageParser;
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
            Dawnloader dawnloader = new Dawnloader();
            string text = dawnloader.Download(@"http://oper.ru/");

            Person person = new Person();
            person.Keywords = new List<Keyword>();
            person.Keywords.Add(new Keyword() { Name = "Путин" });
            person.Keywords.Add(new Keyword() { Name = "Путином" });
            person.Keywords.Add(new Keyword() { Name = "Путина" });
            foreach (string url in PageLinkFinder.FindPageUrls(text))
            {
                Console.WriteLine(url);
            }
            PersonRanker ranker = new PersonRanker(person);
            Console.WriteLine(ranker.GetPersonPageRank(text));
            Console.ReadKey();
        }
    }
}
