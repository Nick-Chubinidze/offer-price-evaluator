using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Entities
{
    public class Item
    {
        public Item()
        {
            ItemTagValues = new HashSet<ItemTagValue>();
            AlternaticePriceItemTags = new HashSet<AlternativePriceItemTag>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime PublishedOn { get; set; }

        public string ExternalId { get; set; }

        public List<byte[]> Image { get; set; }

        public string ContactNumbers { get; set; }

        public string OfferTitle { get; set; }

        public string OfferLocation { get; set; }

        public string ProductDescription { get; set; }

        public bool IsUsed { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }

        public string Url { get; set; } 

        [Required]
        public virtual Category Category { get; set; }

        public virtual ICollection<ItemTagValue> ItemTagValues { get; set; }

        public virtual ICollection<AlternativePriceItemTag> AlternaticePriceItemTags { get; set; }
    }
}
