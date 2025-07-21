namespace JuniorOnly.Application.DTO.Favorite
{
    public class FavoriteDto
    {
        public Guid CandidateProfileId { get; set; }
        public Guid JobOfferId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
