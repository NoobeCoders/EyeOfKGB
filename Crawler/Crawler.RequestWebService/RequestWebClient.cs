using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.RequestWebService
{
    class RequestWebClient : ICustomWebClient, IDisposable
    {        
        public string GetRequest(string url)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    Byte[] pageData = client.DownloadData(url);

                    return Encoding.UTF8.GetString(pageData);
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }                
            }           
        }

        public void PostRequest(string url, string value)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadData(url, "POST", Encoding.UTF8.GetBytes(value));
            }
        }

        public void PutReqest(string url, string value)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadData(url, "PUT", Encoding.UTF8.GetBytes(value));
            }
        }

        public void DeleteRequest(string url, string value)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadData(url, "DELETE", Encoding.UTF8.GetBytes(value));
            }
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
