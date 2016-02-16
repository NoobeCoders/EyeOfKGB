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
        string host;

        public RequestWebClient(string host)
        {
            this.host = host;
        }

        public string Host { get { return host; } }

        public string GetRequest(string api)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    Byte[] pageData = client.DownloadData(host + api);

                    return Encoding.UTF8.GetString(pageData);
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }                
            }           
        }

        public void PostRequest(string api, string value)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadData(host + api, "POST", Encoding.UTF8.GetBytes(value));
            }
        }

        public void PutRequest(string api, string value)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadData(host + api, "PUT", Encoding.UTF8.GetBytes(value));
            }
        }

        public void DeleteRequest(string api, string value)
        {
            using (WebClient client = new WebClient())
            {
                client.UploadData(host + api, "DELETE", Encoding.UTF8.GetBytes(value));
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
