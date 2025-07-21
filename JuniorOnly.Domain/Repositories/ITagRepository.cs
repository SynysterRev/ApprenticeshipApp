using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface ITagRepository : IBaseRepository
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
        /// Delete the tag with the matching ID.
        /// </summary>
        /// <param name="tagId">The tag object to delete.</param>
        public Task DeleteTagAsync(Tag tag);
    }
}
