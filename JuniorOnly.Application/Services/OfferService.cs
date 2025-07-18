using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOffersRepository _offersRepository;

        public OfferService(IOffersRepository offersRepository)
        {
            _offersRepository = offersRepository;
        }

        public async Task<OfferDto> CreateOfferAsync(OfferCreateDto offerDto)
        {
            if (offerDto.SalaryMin > offerDto.SalaryMax)
            {
                throw new ValidationException("The minimal salary cannot be greater than the maximum salary.");
            }

            Offer offer = offerDto.ToEntity();
            offer.Id = Guid.NewGuid();

            await _offersRepository.AddOfferAsync(offer);

            return offer.ToDto();
        }

        public Task<bool> DeleteOfferAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferDto>> GetAllOffersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OfferDto?> GetOfferByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferDto>> GetOffersByCompanyAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferDto>> SearchOffersAsync(string searchTerm, int? experienceMax = null)
        {
            throw new NotImplementedException();
        }

        public Task<OfferDto?> UpdateOfferAsync(Guid id, OfferUpdateDto offerDto)
        {
            throw new NotImplementedException();
        }
    }
}
