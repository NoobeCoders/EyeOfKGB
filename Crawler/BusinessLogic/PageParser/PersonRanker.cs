using Crawler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLogic.PageParser
{
    public class PersonRanker
    {
        Person person;

        public PersonRanker(Person person)
        {
            this.person = person;
        }

        public int GetPersonPageRank(string pageContent)
        {
            int personPageRank = 0;
            string[] pageWords = pageContent.Split(new char[] { ' ', '.', ',', '!', '?', ':', ';', '<', '>' });
            
            foreach (Keyword keyword in person.Keywords)
            {
                personPageRank += CountPageKeywordUsage(keyword, pageWords);
            }

            return personPageRank;
        }

        private int CountPageKeywordUsage(Keyword keyword, string[] pageWords)
        {
            int keywordUsage = 0;

            keywordUsage = pageWords.Count(w => w == keyword.Name);

            return keywordUsage;
        }
    }
}
