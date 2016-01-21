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
                string[] pageWords = bodyNode.InnerHtml.Split(new char[] { ' ', '.', ',', '!', '?', ':', ';', '<', '>' });

                foreach (Keyword keyword in person.Keywords)
                {
                    personPageRank += CountPageKeywordUsage(keyword, pageWords);
                }
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
