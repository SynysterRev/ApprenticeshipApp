using JuniorOnly.Application.DTO.Favorite;
using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.DTO.Pagination;

namespace JuniorOnly.Application.Interfaces
{
    public interface IOfferService
    {
        /// <summary>
        /// Gets all job offers.
        /// </summary>
        /// <returns>List of OfferDto representing all offers.</returns>
        public Task<PaginatedResponse<OfferDto>> GetAllOffersAsync(int pageNumber);

        /// <summary>
        /// Gets a job offer by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the job offer.</param>
        /// <returns>OfferDto if found, otherwise throw a not found exception.</returns>
        public Task<OfferDto> GetOfferByIdAsync(Guid offerId);

        /// <summary>
        /// Gets all job offers for a specific company.
        /// </summary>
        /// <param name="companyId">Guid of the company.</param>
        /// <returns>List of OfferDto for the company.</returns>
        public Task<PaginatedResponse<OfferDto>> GetOffersByCompanyAsync(Guid companyId, int pageNumber);

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
        /// <returns>The updated OfferDto if found, otherwise throw a not found exception.</returns>
        public Task<OfferDto> UpdateOfferAsync(Guid offerId, OfferUpdateDto offerDto);

        /// <summary>
        /// Deletes a job offer.
        /// </summary>
        /// <param name="id">Guid of the offer to delete.</param>
        public Task DeleteOfferAsync(Guid offerId);

        /// <summary>
        /// Deactivate the job offer with the matching ID
        /// </summary>
        /// <param name="offerId">Guid of the offer to deactivate</param>
        public Task SoftDeleteOfferAsync(Guid offerId);

        /// <summary>
        /// Get the number total of offers
        /// </summary>
        /// <returns>The number total of offers</returns>
        public Task<int> GetOffersCountAsync();

        /// <summary>
        /// Get the xth last published offers
        /// </summary>
        /// <param name="count">Number of offers to returns</param>
        /// <returns>List of the last xth OfferDto</returns>
        public Task<List<OfferDto>> GetLastestOffersAsync(int count);

        /// <summary>
        /// Searches job offers by keyword and maximum experience.
        /// </summary>
        /// <param name="query">Filters to apply.</param>
        /// <returns>List of matching OfferDto.</returns>
        public Task<PaginatedResponse<OfferDto>> SearchOffersAsync(OfferSearchQuery query, int pageNumber);

        /// <summary>
        /// Get all favorite job offers for a specific candidate
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <returns>A list of favorite job offers as DTOs</returns>
        public Task<PaginatedResponse<OfferDto>> GetFavoriteOffersAsync(Guid candidateId, int pageNumber);

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