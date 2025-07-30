using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface IOfferRepository : IBaseRepository
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
        /// Delete the job offer with the matching ID
        /// </summary>
        /// <param name="offerId">The job offer object to delete</param>
        public Task DeleteOfferAsync(Offer offer);

        /// <summary>
        /// Get the number total of offers
        /// </summary>
        /// <returns>The total of offers</returns>
        public Task<int> GetOffersCountAsync();

        /// <summary>
        /// Get the xth last published offers
        /// </summary>
        /// <param name="count">Number of offers to returns</param>
        /// <returns>A list of offers</returns>
        public Task<List<Offer>> GetLastestOffersAsync(int count);

        /// <summary>
        /// Search job offers by a search term in the title or description,
        /// and optionally filter by maximum required experience.
        /// </summary>
        /// <param name="searchTerm">The term to search for in the title or description.</param>
        /// <param name="experienceMax">Optional maximum years of experience required.</param>
        /// <returns>A list of job offers matching the criteria.</returns>
        public Task<List<Offer>> SearchOffersAsync(string searchTerm, int? experienceMax = null);

        /// <summary>
        /// Get all favorite job offers for a specific candidate
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <returns>A list of favorite job offers</returns>
        public Task<List<Offer>> GetFavoriteOffersByCandidateAsync(Guid candidateId);

        /// <summary>
        /// Check if a job offer is marked as favorite by a candidate
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <param name="offerId">The ID of the job offer</param>
        /// <returns>True if the offer is a favorite, false otherwise</returns>
        public Task<bool> IsFavoriteAsync(Guid candidateId, Guid offerId);

        /// <summary>
        /// Add a job offer to a candidate's favorites
        /// </summary>
        /// <param name="favorite">The favorite object</param>
        public Task<Favorite> AddFavoriteAsync(Favorite favorite);

        /// <summary>
        /// Remove a job offer from a candidate's favorites
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <param name="offerId">The ID of the job offer</param>
        public Task RemoveFavoriteAsync(Guid candidateId, Guid offerId);

    }
}