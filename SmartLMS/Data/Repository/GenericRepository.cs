using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SmartLMS.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context = null;
        private DbSet<T> _entitySet = null;

        public GenericRepository(ApplicationDbContext Context)
        {
            this._context = Context;
            this._entitySet = Context.Set<T>();
        }

        protected DbSet<T> DbSet
        {
            get
            { return _entitySet; }
        }

        public IQueryable<T> All()
        {
            return DbSet.AsQueryable<T>();
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public T Create(T entity)
        {
            var newentity = DbSet.Add(entity);
            Save();
            return newentity;
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);

        }

        public void Delete(object id)
        {
            T entityToDelete = DbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }                
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }
        
        public virtual T Find(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.FirstOrDefault(predicate);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsEnumerable<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = DbSet;

            query = query.Where(predicate);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            { query = query.Include(includeProperty); }

            return query.ToList();
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this._context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = state;
        }
    }
}