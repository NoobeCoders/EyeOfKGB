using Crawler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Downloader : IDownloader
    {

        public Downloader()
        {
        }

        public async Task<string> Download(string url)
        {

            WebClient client = new WebClient();
            string answer;

            try
            {
                Byte[] pageData = await client.DownloadDataTaskAsync(url);

                answer = Encoding.UTF8.GetString(pageData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine(url);
                answer = ex.Message;
            }

            client.Dispose();

            return answer;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
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
