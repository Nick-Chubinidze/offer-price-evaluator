using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Entities
{
    public class Seller
    {
        public Seller()
        {
            AlternaticePriceItemTags = new HashSet<AlternativePriceItemTag>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AlternativePriceItemTag> AlternaticePriceItemTags { get; set; }
    }
}
