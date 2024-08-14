using Day2.Models;

namespace Book_Store_MVC.IRepositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetByStringId(string id);
    }
}
