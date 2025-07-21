using JuniorOnly.Application.DTO.Application;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICandidateProfileRepository _profileRepository;
        private readonly IOfferRepository _offerRepository;

        public ApplicationService(IApplicationRepository applicationRepository,
            ICandidateProfileRepository profileRepository,
            IOfferRepository offerRepository)
        {
            _applicationRepository = applicationRepository;
            _profileRepository = profileRepository;
            _offerRepository = offerRepository;
        }

        public async Task<ApplicationDto> CreateApplicationAsync(ApplicationCreateDto applicationDto)
        {
            var applicant = await _profileRepository.GetProfileByIdAsync(applicationDto.ApplicantId);

            if (applicant == null)
            {
                throw new NotFoundException($"Profile with ID {applicationDto.ApplicantId} not found");
            }

            var offer = await _offerRepository.GetOfferByIdAsync(applicationDto.OfferId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {applicationDto.OfferId} not found");
            }

            var application = applicationDto.ToEntity();
            application.Id = Guid.NewGuid();

            await _applicationRepository.AddApplicationAsync(application);

            return application.ToDto();
        }

        public async Task DeleteApplicationAsync(Guid applicationId)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
            if (application == null)
            {
                throw new NotFoundException($"Application with ID {applicationId} not found");
            }

            await _applicationRepository.DeleteApplicationAsync(application);
        }

        public async Task<List<ApplicationDto>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllApplicationsAsync();
            return applications.Select(a => a.ToDto()).ToList();
        }

        public async Task<ApplicationDto?> GetApplicationByIdAsync(Guid applicationId)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
            if (application == null)
            {
                throw new NotFoundException($"Application with ID {applicationId} not found");
            }

            return application.ToDto();
        }

        public async Task<List<ApplicationDto>> GetApplicationsByCandidateAsync(Guid candidateId)
        {
            var applications = await _applicationRepository.GetApplicationsByCandidateAsync(candidateId);
            return applications.Select(a => a.ToDto()).ToList();
        }

        public async Task<List<ApplicationDto>> GetApplicationsByOfferAsync(Guid offerId)
        {
            var applications = await _applicationRepository.GetApplicationsByOfferAsync(offerId);
            return applications.Select(a => a.ToDto()).ToList();
        }

        public async Task<ApplicationDto?> UpdateApplicationAsync(Guid applicationId, ApplicationUpdateDto applicationDto)
        {
            var application = await _applicationRepository.GetApplicationByIdAsync(applicationId);
            if (application == null)
            {
                throw new NotFoundException($"Application with ID {applicationId} not found");
            }

            application.UpdateFrom(applicationDto);

            await _applicationRepository.SaveChangesAsync();

            return application.ToDto();
        }
    }
}
