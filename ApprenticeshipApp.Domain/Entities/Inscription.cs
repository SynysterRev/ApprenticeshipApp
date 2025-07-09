using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Domain.Entities
{
    public class Inscription
    {
        [Required]
        [ForeignKey("ApplicationUser")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Session")]
        public Guid SessionId { get; set; }

        public DateTime InscriptionDate { get; set; }
    }
}
