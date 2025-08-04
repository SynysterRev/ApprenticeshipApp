using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Interfaces
{
    public interface ICityService
    {
        /// <summary>
        /// Call external API to retrieve a list of cities based on the query
        /// </summary>
        /// <param name="query">The city searched</param>
        /// <returns>A list of cities based on the query</returns>
        public Task<List<string>> SearchCitiesAsync(string query);
    }
}
