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
        List<DisallowPattern> disallowPatterns;

        public RobotsPageContentHandler(IDataManager dataManager, IParser parser, Site site)
            :base(dataManager, parser)
        {
            disallowPatterns = dataManager.DisallowPatterns.GetBySiteId(site.Id).ToList();
        }

        public override async Task HandleContent(int pageId, string content)
        {
            Page page = await dataManager.Pages.GetByIdAsync(pageId);

            Site site = page.Site;

            Page sitemapPage = GetSitemapPageFromRobots(content);

            if (sitemapPage.URL == String.Empty)
            {
                sitemapPage.URL = "http://" + page.Site.Name + "/sitemap.xml";
            }

            if (await dataManager.Pages.IsNewUrlAsync(sitemapPage.URL))
            {
                lock (dataManager)
                {
                    site.Pages.Add(sitemapPage);

                    dataManager.Sites.Update(site);
                }
            }


            UpdateDisallowPatterns(site, content);
        }

        private void UpdateDisallowPatterns(Site site, string content)
        {
            IEnumerable<string> disallowPatternStrings = parser.GetDisallowPatterns(content);

            foreach (string disallowPatternString in disallowPatternStrings)
            {
                lock (dataManager)
                {
                    DisallowPattern disallowPattern = disallowPatterns.FirstOrDefault(d => d.Pattern == disallowPatternString);

                    if (disallowPattern == null)
                    {
                        disallowPattern = new DisallowPattern() { Pattern = disallowPatternString, Site = site };
                        dataManager.DisallowPatterns.Add(disallowPattern);
                        disallowPatterns.Add(disallowPattern);
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
