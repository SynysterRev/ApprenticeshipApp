using JuniorOnly.Application.DTO.Tag;

namespace JuniorOnly.Application.Interfaces
{
    public interface ITagService
    {
        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns>List of TagDto representing all tags.</returns>
        public Task<List<TagDto>> GetAllTagsAsync();

        /// <summary>
        /// Gets a tag by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the tag.</param>
        /// <returns>TagDto if found, otherwise null.</returns>
        public Task<TagDto?> GetTagByIdAsync(Guid id);

        /// <summary>
        /// Creates a new tag.
        /// </summary>
        /// <param name="name">Name of the tag.</param>
        /// <returns>The created TagDto.</returns>
        public Task<TagDto> CreateTagAsync(string name);

        /// <summary>
        /// Deletes a tag.
        /// </summary>
        /// <param name="id">Guid of the tag to delete.</param>
        /// <returns>True if deleted, false otherwise.</returns>
        public Task<bool> DeleteTagAsync(Guid id);

        /// <summary>
        /// Gets tags associated with a specific offer.
        /// </summary>
        /// <param name="offerId">Guid of the offer.</param>
        /// <returns>List of TagDto for the offer.</returns>
        public Task<List<TagDto>> GetTagsByOfferIdAsync(Guid offerId);
    }
}