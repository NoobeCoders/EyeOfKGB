using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.RequestWebService
{
    public interface ICustomWebClient
    {
        string GetRequest(string url);
        void PostRequest(string url, string value);
        void PutRequest(string url, string value);
        void DeleteRequest(string url, string value);
    }
}
