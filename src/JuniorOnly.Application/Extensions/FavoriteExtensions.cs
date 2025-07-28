using JuniorOnly.Application.DTO.Favorite;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.Extensions
{
    public static class FavoriteExtensions
    {
        public static FavoriteDto ToDto(this Favorite favorite)
        {
            return new FavoriteDto
            {
                CandidateProfileId = favorite.CandidateProfileId,
                JobOfferId = favorite.JobOfferId,
                CreatedAt = favorite.CreatedAt,
            };
        }

        public static Favorite ToEntity(this FavoriteCreateDto createDto)
        {
            return new Favorite
            {
                CandidateProfileId = createDto.CandidateProfileId,
                JobOfferId = createDto.JobOfferId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
