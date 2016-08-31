using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OfferPriceEvaluator
{
    class HtmlWebWrapper : IHtmlWebWrapper
    {
        public HtmlDocument Load(string url)
        {
            try
            {
                var web = new HtmlWeb
                {
                    PreRequest = delegate (HttpWebRequest webRequest)
                    {
                        webRequest.KeepAlive = false;
                        webRequest.ProtocolVersion = HttpVersion.Version10;
                        return true;
                    }
                };

                return web.Load(url);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                    Console.WriteLine("Bad domain name");
                else if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    Console.WriteLine(response.StatusDescription); // "Not Found"
                    if (response.StatusCode == HttpStatusCode.NotFound)
                        Console.WriteLine("Not there!");
                }

                    throw new WebException(ex.Message); 
            }

        }
    }
}
