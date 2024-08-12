using System.Linq.Expressions;

namespace Book_Store_MVC.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includeProperties);
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
