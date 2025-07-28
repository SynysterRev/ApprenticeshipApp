using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.DTO.Offer
{
    public class OfferSearchQuery
    {
        /// <summary>
        /// Keyword to search for.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Optional maximum experience filter.
        /// </summary>
        public int? ExperienceMax { get; set; }
    }
}
