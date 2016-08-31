 
using System.Collections.Generic;
using System.Data.Entity; 

namespace OfferPriceEvaluator.Domain.Abstract
{
    public abstract class BaseRepository<T> where T : class, new()
    {
        protected IDbContextOfferPriceEvaluator _context;

        //public OfferPriceEvaluatorContext Context
        //{
        //    get { return _context; }
        //}

        internal BaseRepository(IDbContextOfferPriceEvaluator t)
        {
            if(_context == null) _context = t; 
        }

        //internal BaseRepository(OfferPriceEvaluatorContext context = null)
        //{
        //    if (context == null) _context = OfferPriceEvaluatorContext.Instance;
        //    _context = context;
        //}

        public virtual T Fetch(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> Set()
        {
            return _context.Set<T>();
        }

        public virtual void Save(T entity)
        {
            Save(_context.Set<T>(), entity);
        }

        public virtual void Delete(int id)
        {
            Delete(Fetch(id));
        }

        public virtual void Delete(T entity)
        {  
            Delete(_context.Set<T>(), entity);
        }

        protected virtual void Save(IDbSet<T> set, T entity)
        {
            var entry = _context.Entry(entity);
            if (entry == null || entry.State == EntityState.Detached) set.Add(entity);
            _context.SaveChanges();
        }

        protected virtual void Delete(IDbSet<T> set, T entity)
        {
            set.Remove(entity);
            _context.SaveChanges();
        }
    }
}
