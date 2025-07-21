using JuniorOnly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Domain.Repositories
{
    public interface ITagRepository
    {
        /// <summary>
        /// Get all tags in the database.
        /// </summary>
        /// <returns>A list of all tags.</returns>
        public Task<List<Tag>> GetAllTagsAsync();

        /// <summary>
        /// Get the tag matching the given ID if any.
        /// </summary>
        /// <param name="tagId">The tag ID.</param>
        /// <returns>The tag if found, or null.</returns>
        public Task<Tag?> GetTagByIdAsync(Guid tagId);

        /// <summary>
        /// Get all tags matching the given list of IDs.
        /// </summary>
        /// <param name="tagIds">The list of tag IDs.</param>
        /// <returns>A list of found tags.</returns>
        public Task<List<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> tagIds);

        /// <summary>
        /// Add a new tag in the database.
        /// </summary>
        /// <param name="newTag">The tag to add.</param>
        /// <returns>The new tag added.</returns>
        public Task<Tag> AddTagAsync(Tag newTag);

        /// <summary>
        /// Update the tag with the same ID.
        /// </summary>
        /// <param name="updatedTag">The tag to update.</param>
        /// <returns>The updated tag or null if not found.</returns>
        public Task<Tag?> UpdateTagAsync(Tag updatedTag);

        /// <summary>
        /// Delete the tag with the matching ID.
        /// </summary>
        /// <param name="tagId">The ID of the tag to delete.</param>
        /// <returns>True if deleted, false otherwise.</returns>
        public Task<bool> DeleteTagAsync(Guid tagId);
    }
}
