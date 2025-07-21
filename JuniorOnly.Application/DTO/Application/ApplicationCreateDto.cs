using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.Application
{
    public class ApplicationCreateDto
    {
        [Required]
        public Guid ApplicantId { get; set; }

        [Required]
        public Guid OfferId { get; set; }
        public string? Message { get; set; }
    }
}
