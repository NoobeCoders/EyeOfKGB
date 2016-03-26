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

            HtmlNode headNode = htmlDocument.DocumentNode.SelectSingleNode("/html/head");

            personPageRank += GetPersonPageHeadRank(headNode);

            HtmlNode bodyNode = htmlDocument.DocumentNode.SelectSingleNode("/html/body");

            personPageRank += GetPersonPageBodyRank(bodyNode);

            return personPageRank;
        }

        private int GetPersonPageHeadRank(HtmlNode headNode)
        {
            int rank = 0;
            Console.WriteLine(headNode.InnerHtml);

            if (headNode != null)
            {
                List<string> pagePhrases = new List<string>();

                foreach (HtmlNode node in headNode.Descendants("meta"))
                {
                    HtmlAttribute atr = node.Attributes.FirstOrDefault(a => a.Name == "content");

                    if (atr != null && atr.Value != null) pagePhrases.Add(atr.Value);
                }

                HtmlNode title = headNode.ChildNodes.FindFirst("title");

                if (title != null && title.InnerText != null) pagePhrases.Add(title.InnerText);

                foreach (Keyword keyword in person.Keywords)
                {
                    rank += CountPageKeywordUsage(keyword, pagePhrases);
                }
            }

            return rank;
        }

        private int GetPersonPageBodyRank(HtmlNode bodyNode)
        {
            int rank = 0;

            if (bodyNode != null)
            {
                List<string> pagePhrases = new List<string>();
                
                foreach (HtmlNode node in bodyNode.Descendants().Where(n => n.Name == "#text"))
                {
                    pagePhrases.Add(node.InnerText);
                }

                foreach (Keyword keyword in person.Keywords)
                {
                    rank += CountPageKeywordUsage(keyword, pagePhrases);
                }
            }

            return rank;
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
