using JuniorOnly.Application.DTO.Account;
using JuniorOnly.Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generate a JwtToken with the data from the user
        /// </summary>
        /// <param name="user">The user who needs the token</param>
        /// <returns>An AuthenticationDto containing the data</returns>
        public Task<AuthenticationDto> CreateJwtToken(ApplicationUser user);
    }
}
