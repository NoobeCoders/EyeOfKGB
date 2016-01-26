using BusinessLogic;
using BusinessLogic.Interfaces;
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
    class HtmlPageContentHandler : PageContentHandler
    {
        IEnumerable<Person> persons;

        public HtmlPageContentHandler(IDataManager dataManager)
            :base(dataManager)
        {
            persons = dataManager.Persons.GetAll().ToList();
        }

        public override void HandleContent(Page page, string htmlContent)
        {
            GetRank(page, htmlContent);
            InsertNewPages(page, htmlContent);
        }

        private void GetRank(Page page, string htmlContent)
        {
            IEnumerable<string> pagePhrases = Parser.GetPagePhrases(htmlContent);

            foreach (Person person in persons)
            {
                int rank = CountRank(person.Keywords, pagePhrases);

                InsertPersonPageRank(person, page, rank);
            }
        }

        private void InsertNewPages(Page page, string htmlContent)
        {
            IEnumerable<string> pageUrls = Parser.GetPageUrls(htmlContent);

            foreach (String url in pageUrls)
            {
                InsertUrl(page.Site, url);
            }
        }

        private void InsertUrl(Site site, String url)
        {
            Page page = dataManager.Pages.GetAll().FirstOrDefault(p => p.URL == url);

            if (page == null)
            {
                dataManager.Pages.Add(new Page()
                {
                    URL = url,
                    Site = site,
                    FoundDateTime = DateTime.Now
                });
            }
        }

        private void InsertPersonPageRank(Person person, Page page, int rank)
        {
            PersonPageRank personPageRank = dataManager.PersonPageRanks.GetById(person.Id, page.Id);

            if (personPageRank != null)
            {
                personPageRank.Rank = rank;
                dataManager.PersonPageRanks.Update(personPageRank);
            }
            else
            {
                dataManager.PersonPageRanks.Add(new PersonPageRank()
                {
                    Person = person,
                    Page = page,
                    Rank = rank
                });
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
