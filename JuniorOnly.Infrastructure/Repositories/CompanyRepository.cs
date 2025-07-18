using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompanyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        public async Task<Company?> UpdateCompanyAsync(Company updatedCompany)
        {
            var foundCompany = await _dbContext.Companies.FindAsync(updatedCompany.Id);
            if (foundCompany == null)
            {
                return null;
            }
            _dbContext.Entry(foundCompany).CurrentValues.SetValues(updatedCompany);
            await _dbContext.SaveChangesAsync();
            return foundCompany;
        }

        public async Task<bool> DeleteCompanyAsync(Guid companyId)
        {
            var foundCompany = await _dbContext.Companies.FindAsync(companyId);
            if (foundCompany == null)
            {
                return false;
            }
            _dbContext.Companies.Remove(foundCompany);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Offer>> GetJobOffersByCompanyAsync(Guid companyId)
        {
            return await _dbContext.Offers.Where(o => o.CompanyId == companyId).ToListAsync();
        }
    }
}