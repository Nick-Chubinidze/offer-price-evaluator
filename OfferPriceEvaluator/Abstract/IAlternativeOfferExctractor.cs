using OfferPriceEvaluator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Abstract
{
    public interface IAlternativeOfferExctractor
    {
        void GetAlternativeOffers(List<IdAndLinks> idAndLinks);
    }
}
