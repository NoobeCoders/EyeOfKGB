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
    public class Parser : IParser
    {
        public IEnumerable<FoundPage> GetFoundPages(string sitemapXML)
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
                                URL = data.Item(0).InnerText,
                                LastModDate = data.Item(1).InnerText
                            });
            }

            return pages;
        }

        public IEnumerable<string> GetDisallowPatterns(string robots, string agent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPagePhrases(string pageHTML)
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

        public IEnumerable<string> GetPageUrls(string pageHTML)
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
                    url = Regex.Replace(url, "^(http|https)://", "");

                    if (url != null) urls.Add(url);
                }
            }

            return urls;
        }

        #region HTMLparser private methods

        private IEnumerable<string> GetPageHeadPhrases(HtmlNode headNode)
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

        private IEnumerable<string> GetPageBodyPhrases(HtmlNode bodyNode)
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
