using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.DTO.JobSector
{
    public class JobSectorUpdateDto
    {
        [StringLength(100)]
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
