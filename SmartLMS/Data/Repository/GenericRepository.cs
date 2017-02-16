using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SmartLMS.Data.Repository
{
    public class GenericRepository<TObject> : IRepository<TObject> where TObject : class
    {
        private ApplicationDbContext _context = null;
        private DbSet<TObject> _entitySet = null;

        public GenericRepository(ApplicationDbContext Context)
        {
            this._context = Context;
            this._entitySet = Context.Set<TObject>();
        }

        public ApplicationDbContext DbContext
        {
            get
            {
                return _context;
            }
        }

        protected DbSet<TObject> DbSet
        {
            get
            { return _entitySet; }
        }

        public IEnumerable<TObject> All()
        {
            return DbSet.AsEnumerable<TObject>();
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public TObject Create(TObject entry)
        {
            var newEntry = DbSet.Add(entry);
            Save();
            return newEntry;
        }

        public void Delete(TObject entry)
        {
            try
            {
                if (entry == null)
                    return;

                if (_context.Entry(entry).State == EntityState.Detached)
                {
                    DbSet.Attach(entry);
                }
                DbSet.Remove(entry);
                Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        public void Delete(object id)
        {
            TObject entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public TObject Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }
        
        public virtual TObject Find(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.FirstOrDefault(predicate);
        }

        public IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.Where(predicate).AsEnumerable<TObject>();
        }

        public IEnumerable<TObject> Get(Expression<Func<TObject, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TObject> query = DbSet;

            query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(TObject entry)
        {
            DbSet.Attach(entry);
            _context.Entry(entry).State = EntityState.Modified;
            Save();
        }
    }
}