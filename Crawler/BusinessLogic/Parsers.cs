using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }


    }
}
