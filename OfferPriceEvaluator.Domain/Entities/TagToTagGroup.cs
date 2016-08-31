using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Entities
{
   public class TagToTagGroup
    {
        [Key]
        public int Id { get; set; }

        public virtual Tag TagId { get; set; }

        public virtual TagGroup TagGroupId { get; set; }
    }
}
