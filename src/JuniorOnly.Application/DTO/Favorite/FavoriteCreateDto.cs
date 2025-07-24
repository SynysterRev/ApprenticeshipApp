using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.Favorite
{
    public class FavoriteCreateDto
    {
        [Required]
        public Guid CandidateProfileId { get; set; }

        [Required]
        public Guid JobOfferId { get; set; }
    }
}
