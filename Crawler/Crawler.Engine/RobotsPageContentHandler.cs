using BusinessLogic.Interfaces;
using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler.Domain.Entities;
using BusinessLogic;

namespace Crawler.Engine
{
    class RobotsPageContentHandler : PageContentHandler
    {
        Object locker;
        public RobotsPageContentHandler(IDataManager dataManager, IParser parser)
            :base(dataManager, parser)
        {
            locker = new Object();
        }

        public override void HandleContent(Page page, string content)
        {
            Site site = page.Site;

            Page sitemapPage = GetSitemapPageFromRobots(content);

            if (sitemapPage.URL == String.Empty)
            {
                sitemapPage.URL = "http://" + page.Site.Name + "/sitemap.xml";
            }

            lock (locker)
            {
                if (site.Pages.FirstOrDefault(p => p.URL == sitemapPage.URL) == null)
                {
                    site.Pages.Add(sitemapPage);

                    lock(dataManager) { dataManager.Sites.Update(site); }
                }
            }

            //UpdateDisallowPatterns(content);
        }

        private void UpdateDisallowPatterns(string content)
        {
            lock(dataManager) { dataManager.DisallowPatterns.Set(parser.GetDisallowPatterns(content)); }
        }

        private Page GetSitemapPageFromRobots(string robots)
        {
            string sitemapUrl = parser.GetSitemapUrl(robots);

            return new Page()
            {
                URL = sitemapUrl,
                FoundDateTime = DateTime.Now,
                LastScanDate = null
            };
        }
    }
}
