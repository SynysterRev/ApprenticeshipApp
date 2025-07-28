using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface ICompanyRepository : IBaseRepository
    {
        /// <summary>
        /// Get all companies.
        /// </summary>
        /// <returns>A list of all companies.</returns>
        public Task<List<Company>> GetAllCompaniesAsync();

        /// <summary>
        /// Get a specific company by its ID.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>The company if found; otherwise, null.</returns>
        public Task<Company?> GetCompanyByIdAsync(Guid companyId);

        /// <summary>
        /// Add a new company.
        /// </summary>
        /// <param name="company">The company to add.</param>
        /// <returns>The created company.</returns>
        public Task<Company> AddCompanyAsync(Company company);

        /// <summary>
        /// Delete a company by its ID.
        /// </summary>
        /// <param name="company">The company object to delete.</param>
        public Task DeleteCompanyAsync(Company company);

        /// <summary>
        /// Get all job offers for a specific company.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>A list of all job offers for the company.</returns>
        public Task<List<Offer>> GetJobOffersByCompanyAsync(Guid companyId);

        /// <summary>
        /// Get all reconversion friendly companies.
        /// </summary>
        /// <returns>A list of all reconversion friendly companies.</returns>
        public Task<List<Company>> GetReconversionFriendlyCompaniesAsync();
    }
}