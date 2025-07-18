using System;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.DTO.Favorite
{
    public class FavoriteCreateDto
    {
        public Guid CandidateProfileId { get; set; }
        public Guid JobOfferId { get; set; }

        /// <summary>
        /// Converts this DTO to a Favorite entity.
        /// </summary>
        /// <returns>New Favorite entity.</returns>
        public Domain.Entities.Favorite ToEntity()
        {
            return new Domain.Entities.Favorite
            {
                CandidateProfileId = CandidateProfileId,
                JobOfferId = JobOfferId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
