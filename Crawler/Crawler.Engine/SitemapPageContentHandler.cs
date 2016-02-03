using BusinessLogic.Interfaces;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Domain.Entities;
using BusinessLogic.Models;
using System.Text.RegularExpressions;
using BusinessLogic;

namespace Crawler.Engine
{
    class SitemapPageContentHandler : PageContentHandler
    {
        List<Page> pages;

        public SitemapPageContentHandler(IDataManager dataManager, IParser parser, Site site)
            :base(dataManager, parser)
        {
            pages = site.Pages.ToList();
        }

        public override void HandleContent(Page page, string content)
        {
            Site site = page.Site;

            AddNewPagesToSiteFromSitemap(site, content);
        }

        private void AddNewPagesToSiteFromSitemap(Site site, string sitemap)
        {
            IEnumerable<string> urls = parser.GetFoundPages(sitemap);

            AddPagesFromUrls(site, urls);
        }

        private void AddPagesFromUrls(Site site, IEnumerable<string> urls)
        {
            lock (dataManager)
            {
                foreach (string url in urls)
                {
                    Page page = new Page()
                    {
                        URL = url,
                        Site = site,
                        FoundDateTime = DateTime.Now
                    };

                    site.Pages.Add(page);
                    //pages.Add(page);
                    //if (pages.FirstOrDefault(p => p.URL == url) == null)
                    //{
                        
                    //}
                }
            }

            //if (urls.Count() == 0)
            //{
            //    string url = "http://" + site.Name;

            //    if (pages.FirstOrDefault(p => p.URL == url) == null)
            //    {
            //        Page page = new Page()
            //        {
            //            URL = url,
            //            Site = site,
            //            FoundDateTime = DateTime.Now
            //        };

            //        site.Pages.Add(page);
            //        pages.Add(page);
            //    }
            //}
        }
    }
}
