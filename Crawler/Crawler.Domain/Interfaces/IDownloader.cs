﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Domain.Interfaces
{
    public interface IDownloader
    {
        string Download(string url);
    }
}