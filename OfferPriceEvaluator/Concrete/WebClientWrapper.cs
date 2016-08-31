using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator
{
    class WebClientWrapper : IWebClientWrapper
    {
        private readonly WebClient _webClient;

        public WebClientWrapper(WebClient webClient)
        {
            _webClient = webClient;
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }

        public byte[] DownloadData(string address)
        {
           return _webClient.DownloadData(address);
        }
    }
}
