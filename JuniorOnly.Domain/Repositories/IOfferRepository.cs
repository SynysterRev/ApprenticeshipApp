using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface IOfferRepository
    {
        /// <summary>
        /// Get all job offers in the database
        /// </summary>
        /// <returns>A list of all job offers</returns>
        public Task<List<Offer>> GetAllOffersAsync();

        /// <summary>
        /// Get all job offers for a specific company
        /// </summary>
        /// <param name="companyId">The ID of the company</param>
        /// <returns>A list of all job offers for the company</returns>
        public Task<List<Offer>> GetOffersByCompanyAsync(Guid companyId);

        /// <summary>
        /// Get the job offer matching the ID if any
        /// </summary>
        /// <param name="offerId">The job offer id</param>
        /// <returns>A job offer if any matching</returns>
        public Task<Offer?> GetOfferByIdAsync(Guid offerId);

        /// <summary>
        /// Add a new job offer in the database
        /// </summary>
        /// <param name="offer">The job offer to add</param>
        /// <returns>The new job offer added</returns>
        public Task<Offer> AddOfferAsync(Offer newOffer);

        /// <summary>
        /// Add a new favorite for an offer
        /// </summary>
        /// <param name="favorite">The favorite to add</param>
        /// <returns>The new favorite added</returns>
        public Task<Favorite> AddFavoriteAsync(Favorite favorite);

        /// <summary>
        /// Update the job offer with the same ID
        /// </summary>
        /// <param name="updatedOffer">The job offer to update</param>
        /// <returns>The updated job offer or null if not found</returns>
        public Task<Offer?> UpdateOfferAsync(Offer updatedOffer);

        /// <summary>
        /// Delete the job offer with the matching ID
        /// </summary>
        /// <param name="offerId">The ID of the job offer to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        public Task<bool> DeleteOfferAsync(Guid offerId);

        /// <summary>
        /// Remove a favorite from an offer
        /// </summary>
        /// <param name="favoriteId">The ID of the favorite to remove</param>
        /// <returns>True if deleted, false otherwise</returns>
        public Task<bool> RemoveFavoriteAsync(Guid favoriteId);

        /// <summary>
        /// Search job offers by a search term in the title or description,
        /// and optionally filter by maximum required experience.
        /// </summary>
        /// <param name="searchTerm">The term to search for in the title or description.</param>
        /// <param name="experienceMax">Optional maximum years of experience required.</param>
        /// <returns>A list of job offers matching the criteria.</returns>
        public Task<List<Offer>> SearchOffersAsync(string searchTerm, int? experienceMax = null);
    }
}