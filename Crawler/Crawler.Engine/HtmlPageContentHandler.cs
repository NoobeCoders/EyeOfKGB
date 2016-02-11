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
        IEnumerable<string> disallowPatternStrings;

        public HtmlPageContentHandler(IDataManager dataManager, IParser parser, Site site)
            :base(dataManager, parser)
        {
            persons = dataManager.Persons.GetAll().ToList();
            disallowPatternStrings = dataManager.DisallowPatterns.GetBySiteId(site.Id).Select(d => d.Pattern).ToList();
        }

        public override async Task HandleContent(int pageId, string htmlContent)
        {
            Page page = await dataManager.Pages.GetByIdAsync(pageId);
            await GetRank(page, htmlContent);
            await InsertNewPages(page, htmlContent);
        }

        private async Task GetRank(Page page, string htmlContent)
        {
            IEnumerable<string> pagePhrases = parser.GetPagePhrases(htmlContent);

            foreach (Person person in persons)
            {
                int rank = CountRank(person.Keywords, pagePhrases);
                if(rank != 0) await InsertPersonPageRank(person, page, rank);
            }
        }

        private async Task InsertNewPages(Page page, string htmlContent)
        {
            IEnumerable<string> pageUrls = parser.GetPageUrls(htmlContent);

            pageUrls = pageUrls.Where(u => u.Contains(page.Site.Name)).ToList();

            FilterUrls(pageUrls, disallowPatternStrings);

            foreach (string url in pageUrls)
            {
                await InsertUrl(page.Site, url);
            }
        }

        private async Task InsertUrl(Site site, string url)
        {
            if (await dataManager.Pages.IsNewUrlAsync(url))
            {
                Page page = new Page()
                {
                    URL = url,
                    Site = site,
                    FoundDateTime = DateTime.Now
                };

                dataManager.Pages.Add(page);
            }
        }

        private async Task InsertPersonPageRank(Person person, Page page, int rank)
        {
            PersonPageRank personPageRank = await dataManager.PersonPageRanks.GetById(person.Id, page.Id);

            if (personPageRank != null)
            {
                personPageRank.Rank = rank;
            }
            else
            {
                lock (dataManager)
                {

                    Console.WriteLine("Идентификатор персоны: " + person.Id + ";" + "Идентификатор страницы: " + page.Id);

                    try
                    {
                        dataManager.PersonPageRanks.Add(new PersonPageRank()
                        {
                            Person = person,
                            Page = page,
                            Rank = rank
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("HtmlHandler" + ";" + ex.Message);
                    }
                }
            }
        }

        private void FilterUrls(IEnumerable<string> urls, IEnumerable<string> disallowPaterns)
        {
            foreach (string disallowPattern in disallowPaterns)
            {
                urls = urls.Where(u => !Regex.IsMatch(u, disallowPattern)).ToList();
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
