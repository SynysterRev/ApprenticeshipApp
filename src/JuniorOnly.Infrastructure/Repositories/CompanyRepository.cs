using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            return await _dbContext.Companies.Include(c => c.JobOffers).ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
        {
            return await _dbContext.Companies.Include(c => c.JobOffers).FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public async Task<Company> AddCompanyAsync(Company company)
        {
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();
            return company;
        }

        public async Task DeleteCompanyAsync(Company company)
        {
            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Offer>> GetJobOffersByCompanyAsync(Guid companyId)
        {
            return await _dbContext.Offers.Where(o => o.CompanyId == companyId).ToListAsync();
        }

        public async Task<List<Company>> GetReconversionFriendlyCompaniesAsync()
        {
            return await _dbContext.Companies.Where(c => c.IsReconversionFriendly).ToListAsync();
        }
    }
}