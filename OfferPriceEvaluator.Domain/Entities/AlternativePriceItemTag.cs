using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Entities
{
    public class AlternativePriceItemTag
    {
        [Key]
        public int Id { get; set; }

        public string Currency { get; set; }

        public Decimal Price { get; set; }

        public string Link { get; set; }

        [Required]
        public virtual Seller Seller { get; set; }

        [Required]
        public virtual Item Item { get; set; }

        [Required]
        public virtual Tag Tag { get; set; }
    }
}
