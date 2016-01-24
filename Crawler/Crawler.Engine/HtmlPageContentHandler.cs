using BusinessLogic.Interfaces;
using Crawler.Domain.Entities;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Engine
{
    class HtmlPageContentHandler : PageContentHandler
    {
        IEnumerable<Person> persons;

        public HtmlPageContentHandler(IDataManager dataManager, IParser parser)
            :base(dataManager, parser)
        {
            persons = dataManager.Persons.GetAll();
        }

        public override void HandleContent(Page page, string htmlContent)
        {
            IEnumerable<string> pagePhrases = parser.GetPagePhrases(htmlContent);

            foreach (Person person in persons)
            {
                int rank = CountRank(person.Keywords, pagePhrases);

                SavePersonPageRank(person, page, rank);
            }
        }

        private void SavePersonPageRank(Person person, Page page, int rank)
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
