using BusinessLogic;
using BusinessLogic.Interfaces;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    class HtmlPageContentHandler : PageContentHandler
    {
        IEnumerable<Person> persons;
        IEnumerable<PersonPageRank> personPageRanks;
        List<Page> pages;

        public HtmlPageContentHandler(IDataManager dataManager, IParser parser)
            :base(dataManager, parser)
        {
            persons = dataManager.Persons.GetAll().ToList();
            personPageRanks = dataManager.PersonPageRanks.GetAll().ToList();
            pages = dataManager.Pages.GetAll().ToList();
        }

        public override void HandleContent(Page page, string htmlContent)
        {
            GetRank(page, htmlContent);
            //InsertNewPages(page, htmlContent);
        }

        private void GetRank(Page page, string htmlContent)
        {
            IEnumerable<string> pagePhrases = parser.GetPagePhrases(htmlContent);

            foreach (Person person in persons)
            {
                int rank = CountRank(person.Keywords, pagePhrases);
                if(rank != 0) InsertPersonPageRank(person, page, rank);
            }
        }

        private void InsertNewPages(Page page, string htmlContent)
        {
            IEnumerable<string> pageUrls = parser.GetPageUrls(htmlContent);

            FilterUrls(pageUrls, dataManager.DisallowPatterns.GetAll().Select(d => d.Pattern).ToList());

            foreach (String url in pageUrls)
            {
                InsertUrl(page.Site, url);
            }
        }

        private void InsertUrl(Site site, String url)
        {
            lock (dataManager)
            {
                Page page = pages.FirstOrDefault(p => p.URL == url);

                if (page == null)
                {
                    page = new Page()
                    {
                        URL = url,
                        Site = site,
                        FoundDateTime = DateTime.Now
                    };

                    dataManager.Pages.Add(page);

                    pages.Add(page);
                }
            }
        }

        private void InsertPersonPageRank(Person person, Page page, int rank)
        {
            PersonPageRank personPageRank = personPageRanks.FirstOrDefault(p => p.PersonId == person.Id && p.PageId == page.Id);

            if (personPageRank != null)
            {
                personPageRank.Rank = rank;
            }
            else
            {
                lock (dataManager)
                {
                    dataManager.PersonPageRanks.Add(new PersonPageRank()
                    {
                        Person = person,
                        Page = page,
                        Rank = rank
                    });
                }
            }
        }

        private void FilterUrls(IEnumerable<String> urls, IEnumerable<String> disallowPaterns)
        {
            foreach (String disallowPattern in disallowPaterns)
            {
                urls = urls.Where(u => !Regex.IsMatch(u, disallowPattern));
            }
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
    }
}
