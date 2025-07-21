using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITagRepository _tagRepository;

        public OfferService(IOfferRepository offersRepository,
            ICompanyRepository companyRepository,
            ITagRepository tagRepository)
        {
            _offerRepository = offersRepository;
            _companyRepository = companyRepository;
            _tagRepository = tagRepository;
        }

        public async Task<OfferDto> CreateOfferAsync(OfferCreateDto offerDto)
        {
            if (offerDto.SalaryMin > offerDto.SalaryMax)
            {
                throw new ValidationException("The minimal salary cannot be greater than the maximum salary.");
            }

            var company = await _companyRepository.GetCompanyByIdAsync(offerDto.CompanyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {offerDto.CompanyId} not found");
            }

            var tags = await _tagRepository.GetTagsByIdsAsync(offerDto.TagIds);

            Offer offer = offerDto.ToEntity();
            offer.Id = Guid.NewGuid();
            offer.Tags = tags;

            await _offerRepository.AddOfferAsync(offer);

            return offer.ToDto();
        }

        public async Task<bool> DeleteOfferAsync(Guid offerId)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            return await _offerRepository.DeleteOfferAsync(offerId);
        }

        public async Task<List<OfferDto>> GetAllOffersAsync()
        {
            var offers = await _offerRepository.GetAllOffersAsync();
            return offers.Select(o => o.ToDto()).ToList();
        }

        public async Task<OfferDto?> GetOfferByIdAsync(Guid offerId)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            return offer.ToDto();
        }

        public async Task<List<OfferDto>> GetOffersByCompanyAsync(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {companyId} not found");
            }

            var offers = await _offerRepository.GetOffersByCompanyAsync(companyId);
            return offers.Select(o => o.ToDto()).ToList();
        }

        public async Task<List<OfferDto>> SearchOffersAsync(string searchTerm, int? experienceMax = null)
        {
            var offers = await _offerRepository.SearchOffersAsync(searchTerm, experienceMax);

            return offers.Select(o => o.ToDto()).ToList();
        }

        public async Task<OfferDto?> UpdateOfferAsync(Guid offerId, OfferUpdateDto offerDto)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            offer.UpdateFrom(offerDto);

            await _offerRepository.UpdateOfferAsync(offer);
            return offer.ToDto();
        }
    }
}
