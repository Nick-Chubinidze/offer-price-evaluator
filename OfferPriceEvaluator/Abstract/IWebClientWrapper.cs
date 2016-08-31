using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator
{
    public interface IWebClientWrapper
    {
        byte[] DownloadData(string address);

        void Dispose();
    }
}
