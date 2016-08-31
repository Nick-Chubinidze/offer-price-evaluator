using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Concrete
{
    public class TagRepository : BaseRepository<Tag>
    {
        public TagRepository(IDbContextOfferPriceEvaluator t) : base(t)
        {

        }
    }
}
