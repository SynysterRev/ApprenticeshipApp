using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class TagRepository : BaseRepository, ITagRepository
    {
        public TagRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Tag> AddTagAsync(Tag newTag)
        {
            _dbContext.Tags.Add(newTag);
            await _dbContext.SaveChangesAsync();
            return newTag;
        }

        public async Task DeleteTagAsync(Tag tag)
        {
            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(Guid tagId)
        {
            return await _dbContext.Tags.Where(t => t.Id == tagId).FirstOrDefaultAsync();
        }

        public async Task<List<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> tagIds)
        {
            if (tagIds == null || !tagIds.Any())
            {
                return new List<Tag>();
            }

            return await _dbContext.Tags
                .Where(t => tagIds.Contains(t.Id))
                .ToListAsync();
        }
    }
}
