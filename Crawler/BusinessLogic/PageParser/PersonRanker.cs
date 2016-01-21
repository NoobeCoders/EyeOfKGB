using Crawler.Domain.Entities;
using HtmlAgilityPack;
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
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContent);

            HtmlNode bodyNode = htmlDocument.DocumentNode.SelectSingleNode("/html/body");
            
            if(bodyNode != null)
            {
                List<string> pagePhrases = new List<string>();

                foreach (HtmlNode node in bodyNode.Descendants())
                {
                    pagePhrases.Add(node.InnerText);
                }

                foreach (Keyword keyword in person.Keywords)
                {
                    personPageRank += CountPageKeywordUsage(keyword, pagePhrases);
                }
            }

            return personPageRank;
        }

        private int CountPageKeywordUsage(Keyword keyword, IEnumerable<string> pagePhrases)
        {
            int keywordUsage = 0;

            foreach (string phrase in pagePhrases)
            {
                keywordUsage += Regex.Matches(phrase, keyword.Name).Count;
            }

            return keywordUsage;
        }
    }
}
