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

namespace Crawler.Engine
{
    class SitemapPageContentHandler : PageContentHandler
    {
        public SitemapPageContentHandler(IDataManager dataManager, IParser parser)
            :base(dataManager, parser)
        {

        }

        public override void HandleContent(Page page, string content)
        {
            Site site = page.Site;

            AddNewPagesToSiteFromSitemap(site, content);

            dataManager.Sites.Update(site);
        }

        private void AddNewPagesToSiteFromSitemap(Site site, string sitemap)
        {
            IEnumerable<string> urls = parser.GetFoundPages(sitemap).Select(p => p.URL).ToList();

            AddPagesFromUrls(site, urls);
        }

        private void AddPagesFromUrls(Site site, IEnumerable<string> urls)
        {
            foreach (string url in urls)
            {
                if (site.Pages.FirstOrDefault(p => p.URL == url) == null)
                {
                    site.Pages.Add(new Page()
                    {
                        URL = url,
                        Site = site,
                        FoundDateTime = DateTime.Now
                    });
                }
            }
        }
        private void FindNewPagesInSitemap(Site site, string sitemap, IEnumerable<string> disallowPattens)
        {
            IEnumerable<FoundPage> foundPages = parser.GetFoundPages(sitemap);

            List<string> allowPageURLs = new List<string>();

            allowPageURLs = foundPages.Select(p => p.URL).ToList();

            foreach (string disallowPattern in disallowPattens)
            {
                allowPageURLs = allowPageURLs.Where(u => !Regex.IsMatch(u, disallowPattern)).ToList();
            }

            AddPagesFromUrls(site, allowPageURLs);
        }
    }
}
