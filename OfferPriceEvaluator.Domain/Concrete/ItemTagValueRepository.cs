using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;

namespace OfferPriceEvaluator.Domain.Concrete
{
    public class ItemTagValueRepository : BaseRepository<ItemTagValue>
    {
        public ItemTagValueRepository(IDbContextOfferPriceEvaluator t) : base(t)
        {
        }
    }
}
