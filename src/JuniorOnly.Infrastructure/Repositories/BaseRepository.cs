using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
