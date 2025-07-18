using System;

namespace JuniorOnly.Application.DTO.Favorite
{
    public class FavoriteCreateDto
    {
        public Guid CandidateProfileId { get; set; }
        public Guid JobOfferId { get; set; }
    }
}
