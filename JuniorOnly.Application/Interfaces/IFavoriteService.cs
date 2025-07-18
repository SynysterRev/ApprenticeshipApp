using JuniorOnly.Application.DTO.Favorite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Interfaces
{
    public interface IFavoriteService
    {
        /// <summary>
        /// Gets all favorites for a candidate profile.
        /// </summary>
        /// <param name="profileId">Guid of the candidate profile.</param>
        /// <returns>List of FavoriteDto for the profile.</returns>
        public Task<List<FavoriteDto>> GetFavoritesByProfileIdAsync(Guid profileId);

        /// <summary>
        /// Creates a new favorite.
        /// </summary>
        /// <param name="favoriteDto">FavoriteCreateDto containing favorite data.</param>
        /// <returns>The created FavoriteDto.</returns>
        public Task<FavoriteDto> CreateFavoriteAsync(FavoriteCreateDto favoriteDto);

        /// <summary>
        /// Deletes a favorite.
        /// </summary>
        /// <param name="id">Guid of the favorite to delete.</param>
        /// <returns>True if deleted, false otherwise.</returns>
        public Task<bool> DeleteFavoriteAsync(Guid id);

        /// <summary>
        /// Checks if an offer is a favorite for a given profile.
        /// </summary>
        /// <param name="profileId">Guid of the candidate profile.</param>
        /// <param name="offerId">Guid of the job offer.</param>
        /// <returns>True if the offer is a favorite, false otherwise.</returns>
        public Task<bool> IsFavoriteAsync(Guid profileId, Guid offerId);
    }
}