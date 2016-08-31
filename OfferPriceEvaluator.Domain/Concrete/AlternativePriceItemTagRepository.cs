using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;

namespace OfferPriceEvaluator.Domain.Concrete
{
   public class AlternativePriceItemTagRepository : BaseRepository<AlternativePriceItemTag>
    {
       public AlternativePriceItemTagRepository(IDbContextOfferPriceEvaluator t) : base(t)
       {
       }
    }
}
