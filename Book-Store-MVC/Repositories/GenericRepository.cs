using Book_Store_MVC.IRepositories;
using Book_Store_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Book_Store_MVC.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private BookStoreContext Context;
        private DbSet<T> DBset; 
        public GenericRepository()
        {
            Context = new BookStoreContext();
            DBset = Context.Set<T>();
        }
        public GenericRepository(BookStoreContext _contex)
        {
            Context = _contex;
            DBset=_contex.Set<T>();
        }
        public void Add(T item)
        {
            DBset.Add(item);
        }

        public void Delete(T item)
        {
            DBset.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {   
            
            return DBset.ToList();
        }
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DBset;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public T GetById(int id)
        {
            return DBset.Find(id);
        }

        public void Update(T item)
        {
            DBset.Update(item);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
