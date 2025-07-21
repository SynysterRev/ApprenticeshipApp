using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OfferRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Offer>> GetAllOffersAsync()
        {
            return await _dbContext.Offers.Include(o => o.Company).Include(o => o.Tags).ToListAsync();
        }

        public async Task<List<Offer>> GetOffersByCompanyAsync(Guid companyId)
        {
            return await _dbContext.Offers.Where(o => o.CompanyId == companyId).Include(o => o.Tags).ToListAsync();
        }

        public async Task<Offer?> GetOfferByIdAsync(Guid offerId)
        {
            return await _dbContext.Offers.Include(o => o.Company).Include(o => o.Tags).FirstOrDefaultAsync(o => o.Id == offerId);
        }

        public async Task<Offer> AddOfferAsync(Offer newOffer)
        {
            _dbContext.Offers.Add(newOffer);
            await _dbContext.SaveChangesAsync();
            return newOffer;
        }

        public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<Offer?> UpdateOfferAsync(Offer updatedOffer)
        {
            var foundOffer = await _dbContext.Offers.FindAsync(updatedOffer.Id);
            if (foundOffer == null)
            {
                return null;
            }
            _dbContext.Entry(foundOffer).CurrentValues.SetValues(updatedOffer);
            await _dbContext.SaveChangesAsync();
            return foundOffer;
        }

        public async Task<bool> DeleteOfferAsync(Guid offerId)
        {
            var foundOffer = await _dbContext.Offers.FindAsync(offerId);
            if (foundOffer == null)
            {
                return false;
            }
            _dbContext.Offers.Remove(foundOffer);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFavoriteAsync(Guid favoriteId)
        {
            var favorite = await _dbContext.Favorites.FindAsync(favoriteId);
            if (favorite == null)
            {
                return false;
            }
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Offer>> SearchOffersAsync(string searchTerm, int? experienceMax = null)
        {
            var query = _dbContext.Offers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                query = query.Where(o => o.Title.ToLower().Contains(searchTerm) || o.Description.ToLower().Contains(searchTerm));
            }

            if (experienceMax.HasValue && experienceMax >= 0)
            {
                query = query.Where(o => o.ExperienceRequired <= experienceMax.Value);
            }

            return await query.ToListAsync();
        }
    }
}