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
            client.Timeout = 2 * 1000;

            client.Headers.Remove(HttpRequestHeader.Accept);
            client.Headers.Remove(HttpRequestHeader.UserAgent);
            client.Headers.Remove(HttpRequestHeader.AcceptLanguage);
            client.Headers.Remove(HttpRequestHeader.AcceptEncoding);
            client.Headers.Remove(HttpRequestHeader.CacheControl);
            client.Headers.Remove(HttpRequestHeader.Pragma);
            //client.Headers.Remove(HttpRequestHeader.Connection);

            //client.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            client.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
            client.Headers.Add(HttpRequestHeader.Pragma, "no-cache");
            client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
            client.Headers.Add(HttpRequestHeader.AcceptLanguage, "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
            client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
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
            WebRequest webRequest = null;
            try
            {
                webRequest = base.GetWebRequest(uri);
                webRequest.Timeout = Timeout;
                HttpWebRequest request = (HttpWebRequest)webRequest;
                request.ReadWriteTimeout = Timeout;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            catch (Exception ex)
            {

            }

            return webRequest;
        }
    }
}
