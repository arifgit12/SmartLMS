using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartLMS.Data.Repository
{
    public class SmartLMSData : ISmartLMSData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories = new Dictionary<Type, object>();

        public SmartLMSData()
            : this(new ApplicationDbContext())
        {

        }

        public SmartLMSData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<Lecture> Lectures
        {
            get
            {
                return this.GetRepository<Lecture>();
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeOfRepository, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}