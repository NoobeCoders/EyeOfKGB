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

            CustomWebClient client = new CustomWebClient();
            client.Timeout = 60 * 1000;

            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml");
            headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
            headers.Add(HttpRequestHeader.AcceptLanguage, "u-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
            headers.Add(HttpRequestHeader.Connection, "keep-alive");
            headers.Add(HttpRequestHeader.Pragma, "no-cache");
            headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36");
            
            client.Headers.Remove(HttpRequestHeader.UserAgent);
            client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36");
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

    public class CustomWebClient : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest webRequest = base.GetWebRequest(uri);
            webRequest.Timeout = Timeout;
            ((HttpWebRequest)webRequest).ReadWriteTimeout = Timeout;
            return webRequest;
        }
    }
}
