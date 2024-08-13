using Book_Store_MVC.Models;

namespace Book_Store_MVC.IRepositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IQueryable<Book> GetAll( int id, string searchTerm, int pageNumber, int pageSize);
    }
}
