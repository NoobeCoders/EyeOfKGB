﻿using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IParser
    {
        IEnumerable<string> GetFoundPages(string sitemap);
        IEnumerable<string> GetDisallowPatterns(string robots, string agent);
        IEnumerable<string> GetDisallowPatterns(string robots);
        IEnumerable<string> GetPagePhrases(string pageHTML);
        IEnumerable<string> GetPageUrls(string pageHTML);
        string GetSitemapUrl(string robots);
    }
}