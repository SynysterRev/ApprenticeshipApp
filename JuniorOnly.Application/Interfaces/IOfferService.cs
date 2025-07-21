using JuniorOnly.Application.DTO.Favorite;
using JuniorOnly.Application.DTO.Offer;

namespace JuniorOnly.Application.Interfaces
{
    public interface IOfferService
    {
        /// <summary>
        /// Gets all job offers.
        /// </summary>
        /// <returns>List of OfferDto representing all offers.</returns>
        public Task<List<OfferDto>> GetAllOffersAsync();

        /// <summary>
        /// Gets a job offer by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the job offer.</param>
        /// <returns>OfferDto if found, otherwise null.</returns>
        public Task<OfferDto?> GetOfferByIdAsync(Guid offerId);

        /// <summary>
        /// Gets all job offers for a specific company.
        /// </summary>
        /// <param name="companyId">Guid of the company.</param>
        /// <returns>List of OfferDto for the company.</returns>
        public Task<List<OfferDto>> GetOffersByCompanyAsync(Guid companyId);

        /// <summary>
        /// Creates a new job offer.
        /// </summary>
        /// <param name="offerDto">OfferCreateDto containing offer data.</param>
        /// <returns>The created OfferDto.</returns>
        public Task<OfferDto> CreateOfferAsync(OfferCreateDto offerDto);

        /// <summary>
        /// Updates an existing job offer.
        /// </summary>
        /// <param name="id">Guid of the offer to update.</param>
        /// <param name="offerDto">OfferUpdateDto with updated data.</param>
        /// <returns>The updated OfferDto if found, otherwise null.</returns>
        public Task<OfferDto?> UpdateOfferAsync(Guid offerId, OfferUpdateDto offerDto);

        /// <summary>
        /// Deletes a job offer.
        /// </summary>
        /// <param name="id">Guid of the offer to delete.</param>
        public Task DeleteOfferAsync(Guid offerId);

        /// <summary>
        /// Searches job offers by keyword and maximum experience.
        /// </summary>
        /// <param name="searchTerm">Keyword to search for.</param>
        /// <param name="experienceMax">Optional maximum experience filter.</param>
        /// <returns>List of matching OfferDto.</returns>
        public Task<List<OfferDto>> SearchOffersAsync(string searchTerm, int? experienceMax = null);

        /// <summary>
        /// Get all favorite job offers for a specific candidate
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <returns>A list of favorite job offers as DTOs</returns>
        public Task<List<OfferDto>> GetFavoriteOffersAsync(Guid candidateId);

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
        /// <param name="createDto">FavoriteCreateDto containing the favorite data</param>
        /// <returns>The created favorite if not already existing</returns>
        public Task<FavoriteDto?> AddToFavoritesAsync(FavoriteCreateDto createDto);

        /// <summary>
        /// Remove a job offer from a candidate's favorites
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <param name="offerId">The ID of the job offer</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public Task RemoveFromFavoritesAsync(Guid candidateId, Guid offerId);
    }
}