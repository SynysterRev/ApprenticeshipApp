using ApprenticeshipApp.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Domain.Entities
{
    public class Note
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        [Required]
        [Range(1, 20)]
        public int Grade { get; set; }

        public string? Description { get; set; }

        public ApplicationUser Apprentice { get; set; } = null!;

        public Session Session { get; set; } = null!;
    }
}
