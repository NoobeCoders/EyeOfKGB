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
        public RobotsPageContentHandler(IDataManager dataManager)
            :base(dataManager)
        {

        }

        public override void HandleContent(Page page, string content)
        {
            Site site = page.Site;

            Page sitemapPage = GetSitemapPageFromRobots(content);

            if (site.Pages.FirstOrDefault(p => p.URL == sitemapPage.URL) == null)
            {
                site.Pages.Add(sitemapPage);

                dataManager.Sites.Update(site);
            }
        }

        private Page GetSitemapPageFromRobots(string robots)
        {
            string sitemapUrl = Parser.GetSitemapUrl(robots);

            return new Page()
            {
                URL = sitemapUrl,
                FoundDateTime = DateTime.Now,
                LastScanDate = null
            };
        }
    }
}
