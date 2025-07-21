using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tag> AddTagAsync(Tag newTag)
        {
            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();
            return newTag;
        }

        public async Task<bool> DeleteTagAsync(Guid tagId)
        {
            var foundTag = await _context.Tags.FindAsync(tagId);

            if (foundTag == null)
            {
                return false;
            }
            _context.Tags.Remove(foundTag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(Guid tagId)
        {
            return await _context.Tags.Where(t => t.Id == tagId).FirstOrDefaultAsync();
        }

        public async Task<List<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> tagIds)
        {
            if (tagIds == null || !tagIds.Any())
            {
                return new List<Tag>();
            }

            return await _context.Tags
                .Where(t => tagIds.Contains(t.Id))
                .ToListAsync();
        }

        public async Task<Tag?> UpdateTagAsync(Tag updatedTag)
        {
            var foundTag = await _context.Tags.FindAsync(updatedTag.Id);
            if(foundTag == null)
            {
                return null;
            }
            _context.Entry(foundTag).CurrentValues.SetValues(updatedTag);
            await _context.SaveChangesAsync();

            return foundTag;
        }
    }
}
