using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Downloader
    {
        public async string Download(String url)
        {
            WebClient client;
            string answer;

            try
            {
                using (client = new WebClient())
                {
                    Byte[] pageData = await client.DownloadDataAsync(url);

                    answer = Encoding.ASCII.GetString(pageData);
                }
            }
            catch (Exception ex)
            {
                answer = ex.Message;
            }

            return answer;
        }
    }
}
