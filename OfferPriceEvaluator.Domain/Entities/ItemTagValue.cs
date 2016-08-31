using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Entities
{
    public class ItemTagValue
    {
        public virtual Item Items { get; set; }
        public virtual Tag Tags { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ItemID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TagID { get; set; }

        public string Value { get; set; }
    }
}
