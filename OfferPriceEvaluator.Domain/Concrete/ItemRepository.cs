using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Concrete
{
    public class ItemRepository : BaseRepository<Item>
    {
        public ItemRepository(IDbContextOfferPriceEvaluator t) : base(t)
        {

        }
    }
}
