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
    }
}