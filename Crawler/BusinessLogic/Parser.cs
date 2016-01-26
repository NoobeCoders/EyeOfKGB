using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace BusinessLogic
{
    public static class Parser
    {
        public static IEnumerable<FoundPage> GetFoundPages(string sitemapXML)
        {
            List<FoundPage> pages = new List<FoundPage>();

            XmlDocument sitemap = new XmlDocument();
            sitemap.LoadXml(sitemapXML);

            XmlElement root = sitemap.DocumentElement;

            XmlNodeList urls = root.GetElementsByTagName("url");
            foreach (XmlNode url in urls)
            {
                var data = url.ChildNodes;

                pages.Add(  new FoundPage()
                            {
                                URL = Regex.Replace(data.Item(0).InnerText, "^(http|https)://", String.Empty),
                                LastModDate = DateTime.Parse(data.Item(1).InnerText)
                            });
            }

            return pages;
        }

        public static IEnumerable<string> GetDisallowPatterns(string robots, string agent)
        {
            List<string> dissalowPages = new List<string>();

            List<string> stringsOfRobots = (robots.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToList();

            int i = 0;
            string[] str = new string[2];

            while (!stringsOfRobots[i].Contains("User-agent: " + agent) && !stringsOfRobots[i].Contains("User-agent: *"))
            {
                i++;
            }

            while (i < stringsOfRobots.Count)
            {
                for (int n = i; i < stringsOfRobots.Count; n++)
                {
                    if (stringsOfRobots[i] == "User-agent: " + agent || stringsOfRobots[i] == "User-agent: *")
                    {
                        i++;
                        while (i < stringsOfRobots.Count() && !stringsOfRobots[i].Contains("User-agent:"))
                        {
                            if (stringsOfRobots[i].Contains("Allow"))
                            {
                                i++;
                            }
                            else {
                                if (stringsOfRobots[i].Contains("Disallow"))
                                {
                                    str = stringsOfRobots[i].Split(':');
                                    dissalowPages.Add(str[1]);
                                    i++;
                                }
                                else
                                {
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
            return dissalowPages;
        }


        public string GetSitemapUrl(string robots)
        {
            List<string> stringsOfRobots = (robots.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).ToList();

            string sitemap = "";
            string[] spl = new string[2];

            foreach (var str in stringsOfRobots)
            {
                if (str.Contains("Sitemap:"))
                {
                    spl = str.Split("Sitemap:".ToCharArray());
                    sitemap += spl[1];
                }
            }

            if (sitemap == "")
            {
                foreach (var str in stringsOfRobots)
                {
                    if (str.Contains("Host:"))
                    {
                        spl = str.Split(':');
                        sitemap += spl[1];
                        sitemap += "/sitemap.xml";
                    }
                }
            }

            return sitemap;
        }

        public static string GetSitemapUrl(string robots)
        {
            return "localhost:53432/sitemap.xml";
        }

        public static IEnumerable<string> GetPagePhrases(string pageHTML)
        {
            List<string> pagePhrases = new List<string>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageHTML);

            HtmlNode headNode = htmlDocument.DocumentNode.SelectSingleNode("/html/head");

            pagePhrases.AddRange(GetPageHeadPhrases(headNode));

            HtmlNode bodyNode = htmlDocument.DocumentNode.SelectSingleNode("/html/body");

            pagePhrases.AddRange(GetPageBodyPhrases(bodyNode));

            return pagePhrases;
        }

        public static IEnumerable<string> GetPageUrls(string pageHTML)
        {
            List<string> urls = new List<string>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageHTML);

            IEnumerable<HtmlNode> aNodes = htmlDocument.DocumentNode.Descendants("a");

            foreach (HtmlNode aNode in aNodes)
            {
                HtmlAttribute atr = aNode.Attributes.FirstOrDefault(a => a.Name == "href");
                if (atr != null)
                {
                    string url = atr.Value;
                    url = Regex.Replace(url, "^(http|https)://", String.Empty);

                    if (url != null) urls.Add(url);
                }
            }

            return urls;
        }

        #region HTMLparser private methods

        private static IEnumerable<string> GetPageHeadPhrases(HtmlNode headNode)
        {
            List<string> headPhrases = new List<string>();

            if (headNode != null)
            {
                foreach (HtmlNode node in headNode.Descendants("meta"))
                {
                    HtmlAttribute atr = node.Attributes.FirstOrDefault(a => a.Name == "content");

                    if (atr != null && atr.Value != null) headPhrases.Add(atr.Value);
                }

                HtmlNode title = headNode.ChildNodes.FindFirst("title");

                if (title != null && title.InnerText != null) headPhrases.Add(title.InnerText);
            }

            return headPhrases;
        }

        private static IEnumerable<string> GetPageBodyPhrases(HtmlNode bodyNode)
        {
            List<string> bodyPhrases = new List<string>();

            if (bodyNode != null)
            {

                foreach (HtmlNode node in bodyNode.Descendants().Where(n => n.Name == "#text"))
                {
                    bodyPhrases.Add(node.InnerText);
                }
            }

            return bodyPhrases;
        }
        #endregion
    }
}
