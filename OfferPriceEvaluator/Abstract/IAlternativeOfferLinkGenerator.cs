using System.Collections.Generic;
using System.Threading.Tasks;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Helpers;

namespace OfferPriceEvaluator
{
    public interface IAlternativeOfferLinkGenerator
    {
        List<IdAndLinks> SearchToComparePrice(List<Item> xe); 
    }
}
