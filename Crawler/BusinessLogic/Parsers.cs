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
    public class Parsers
    {
        public ICollection<SitemapPage> GetSiteMap(string sitemapXML)
        {
            List<SitemapPage> pages = new List<SitemapPage>();

            XmlDocument sitemap = new XmlDocument();
            sitemap.LoadXml(sitemapXML);

            XmlElement root = sitemap.DocumentElement;

            XmlNodeList urls = root.GetElementsByTagName("url");
            foreach (XmlNode url in urls)
            {
                var data = url.ChildNodes;

                pages.Add(  new SitemapPage()
                            {
                                URL = data.Item(0).InnerText,
                                Date = data.Item(1).InnerText
                            });
            }

            return pages;
        }
    }
}
