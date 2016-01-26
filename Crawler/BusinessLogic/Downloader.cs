using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Downloader : IDownloader
    {
        WebClient client;

        public Downloader()
        {
            client = new WebClient();
        }

        public string Download(string url)
        {
            string answer;

            try
            {
                Byte[] pageData = client.DownloadData(url);

                answer = Encoding.UTF8.GetString(pageData);
            }
            catch (Exception ex)
            {
                answer = ex.Message;
            }

            return answer;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    client.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
