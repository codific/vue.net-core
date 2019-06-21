using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IRepositoryInjection
    {
        IRepositoryInjection SetContext(DbContext context);
    }
}
