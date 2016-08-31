using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfferPriceEvaluator.Domain.Concrete;

namespace OfferPriceEvaluator.Domain
{
    public class OfferPriceEvaluatorContext : DbContext, IDbContextOfferPriceEvaluator
    {
        private static OfferPriceEvaluatorContext _instance;

        public static OfferPriceEvaluatorContext Instance => _instance ?? (_instance = new OfferPriceEvaluatorContext());

        public OfferPriceEvaluatorContext()
            : base("name=OfferPriceEvaluator")
        {
            Database.SetInitializer<OfferPriceEvaluatorContext>(new CreateDatabaseIfNotExists<OfferPriceEvaluatorContext>());
        }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public IDbSet<ItemTagValue> ItemTagValues { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<TagGroup> TagGroups { get; set; }

        public IDbSet<TagToTagGroup> TagToTagGroups { get; set; }

        public IDbSet<AlternativePriceItemTag> AlternativePriceItemTags { get; set; }

        public IDbSet<Seller> Sellers { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
