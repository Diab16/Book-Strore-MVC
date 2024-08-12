using Book_Store_MVC.Models;

namespace Book_Store_MVC.IRepositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IEnumerable<Book> GetAll(int categoryId, string searchTerm, int pageNumber, int pageSize);
    }
}
