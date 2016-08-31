using OfferPriceEvaluator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator.Domain.Abstract
{
    public interface IDbContextOfferPriceEvaluator 
    {
        IDbSet<Category> Categories { get; set; }

        IDbSet<Item> Items { get; set; }

        IDbSet<ItemTagValue> ItemTagValues { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<TagGroup> TagGroups { get; set; }

        IDbSet<TagToTagGroup> TagToTagGroups { get; set; }  
        IDbSet<Seller> Sellers { get; set; }

        IDbSet<AlternativePriceItemTag> AlternativePriceItemTags { get; set; } 

        DbContextConfiguration Configuration { get; }

        Task<int> SaveChangesAsync();

        //DbSet Set(Type entityType);

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbEntityEntry Entry(object entity);

        void Dispose();
    }
}
