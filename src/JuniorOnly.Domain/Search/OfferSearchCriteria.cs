using JuniorOnly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Domain.Search
{
    public class OfferSearchCriteria
    {
        /// <summary>
        /// Keyword to search for.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Optional contract type filter.
        /// </summary>
        public ContractType? ContractType { get; set; }

        /// <summary>
        /// Optional remote type filter.
        /// </summary>
        public RemoteType? RemoteType { get; set; }

        /// <summary>
        /// Optional minimum salary filter.
        /// </summary>
        public int? MinSalary { get; set; }

        /// <summary>
        /// Optional maximum salary filter.
        /// </summary>
        public int? MaxSalary { get; set; }
    }
}
