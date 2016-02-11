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
        public SitemapPageContentHandler(IDataManager dataManager, IParser parser, Site site)
            :base(dataManager, parser)
        {

        }

        public override async Task HandleContent(int pageId, string content)
        {
            Page page = await dataManager.Pages.GetByIdAsync(pageId);
            Site site = page.Site;

            await AddNewPagesToSiteFromSitemap(site, content);
        }

        private async Task AddNewPagesToSiteFromSitemap(Site site, string sitemap)
        {
            IEnumerable<string> urls = parser.GetFoundPages(sitemap);

            await AddPagesFromUrls(site, urls);
        }

        private async Task AddPagesFromUrls(Site site, IEnumerable<string> urls)
        {
            foreach (string url in urls)
            {

                if (await dataManager.Pages.IsNewUrlAsync(url))
                {
                    lock (dataManager)
                    {
                        Page page = new Page()
                        {
                            URL = url,
                            Site = site,
                            FoundDateTime = DateTime.Now
                        };

                        site.Pages.Add(page);
                    }
                }
                
            }

            if (urls.Count() == 0)
            {
                string url = "http://" + site.Name;

                if (await dataManager.Pages.IsNewUrlAsync(url))
                {
                    lock (dataManager)
                    {
                        Page page = new Page()
                        {
                            URL = url,
                            Site = site,
                            FoundDateTime = DateTime.Now
                        };

                        site.Pages.Add(page);
                    }
                }
            }
        }
    }
}
