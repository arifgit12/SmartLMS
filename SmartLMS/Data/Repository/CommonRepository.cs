using SmartLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartLMS.Data.Repository
{
    public class CommonRepository<T> : IDisposable where T : class
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public void Create(T obj)
        {
            IRepository<T> repository = new GenericRepository<T>(context);
            repository.Add(obj);
            repository.Save();
        }

        public T CreateAndGet(T obj)
        {
            IRepository<T> repository = new GenericRepository<T>(context);
            var objDTO = repository.Add(obj);
            repository.Save();
            return objDTO;
        }

        public T Get(int id)
        {
            IRepository<T> repository = new GenericRepository<T>(context);
            return repository.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            IRepository<T> repository = new GenericRepository<T>(context);

            if (repository.All().Any())
            {
                return repository.All().ToList();
            }

            return new List<T>();
        }        

        public void Update(T obj)
        {
            IRepository<T> repository = new GenericRepository<T>(context);
            repository.Update(obj);
            repository.Save();
        }

        public int Count
        {
            get
            {
                IRepository<T> repository = new GenericRepository<T>(context);
                return repository.Count();
            }
        }

        public void Delete(T obj)
        {
            IRepository<T> repository = new GenericRepository<T>(context);
            repository.Delete(obj);
            repository.Save();
        }

        public void DeleteById(int id)
        {
            IRepository<T> repository = new GenericRepository<T>(context);
            repository.Delete(id);
            repository.Save();
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }


    }
}