using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using SearchEngine.Interfaces;
using SearchEngine.Properties;

namespace SearchEngine.Helpers
{
    public class WebPost : IWebPost
    {
        public string GetHtmlResponse(string url, ISearchInput searchInput)
        {
            try
            {
                string data = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(url));

                if (Convert.ToBoolean(Resources.RequireProxy))
                {
                    ICredentials credentials = new NetworkCredential(Resources.ProxyUsername, Resources.ProxyPassword);
                    WebProxy wp = new WebProxy(Resources.ProxyURL, true, null, credentials);
                    request.Proxy = wp;
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}








