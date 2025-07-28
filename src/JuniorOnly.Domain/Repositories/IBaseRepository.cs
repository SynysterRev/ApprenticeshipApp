namespace JuniorOnly.Domain.Repositories
{
    public interface IBaseRepository
    {
        public Task SaveChangesAsync();
    }
}
