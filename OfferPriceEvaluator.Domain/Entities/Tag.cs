using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Entities
{
    public class Tag
    {
        public Tag()
        {
            ItemTagValues = new HashSet<ItemTagValue>();
            AlternativePriceItemTags = new HashSet<AlternativePriceItemTag>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameForSearch { get; set; }

        public virtual ICollection<ItemTagValue> ItemTagValues { get; set; }

        public virtual ICollection<AlternativePriceItemTag> AlternativePriceItemTags { get; set; }
    }
}
