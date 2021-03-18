using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.Properties;
using SearchEngine.RequestInput;

namespace SearchEngine.ServiceLayers
{
    public class WebPost : IWebPost
    {
        private readonly ILogger<WebPost> ilogger;
        public WebPost(ILogger<WebPost> ilogger)
        {
            this.ilogger = ilogger;
        }
        public string GetHtmlResponse(string url, SearchInput searchInput)
        {
            try
            {
                var result = string.Empty;
                var request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(url));
                if (Convert.ToBoolean(Resources.RequireProxy))
                    request.Proxy = new WebProxy(Resources.ProxyURL, true, null, new NetworkCredential(Resources.ProxyUsername, Resources.ProxyPassword));
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                ilogger.LogError(ex, ex.InnerException.Message);
                throw;
            }
        }

    }
}








