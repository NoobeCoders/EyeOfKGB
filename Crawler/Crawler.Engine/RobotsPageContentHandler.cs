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
        public RobotsPageContentHandler(IDataManager dataManager, IParser parser)
            :base(dataManager, parser)
        {

        }

        public override void HandleContent(Page page, string content)
        {
            Site site = page.Site;

            Page sitemapPage = GetSitemapPageFromRobots(content);

            if (sitemapPage.URL == String.Empty)
            {
                sitemapPage.URL = "http://" + page.Site.Name + "/sitemap.xml";
            }

            lock (dataManager)
            {
                if (site.Pages.FirstOrDefault(p => p.URL == sitemapPage.URL) == null)
                {
                    site.Pages.Add(sitemapPage);

                    dataManager.Sites.Update(site);
                }
            }

            //UpdateDisallowPatterns(site, content);
        }

        private void UpdateDisallowPatterns(Site site, string content)
        {
            foreach (string disallowPatternString in parser.GetDisallowPatterns(content))
            {
                lock (dataManager)
                {
                    DisallowPattern disallowPattern = dataManager.DisallowPatterns.GetAll().FirstOrDefault(d => d.Pattern == disallowPatternString && d.Site == site);

                    if (disallowPattern == null)
                    {
                        dataManager.DisallowPatterns.Add(new DisallowPattern() { Pattern = disallowPatternString, Site = site });
                    }
                }
            }
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
