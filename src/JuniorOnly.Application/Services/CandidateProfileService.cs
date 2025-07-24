using JuniorOnly.Application.DTO.CandidateProfile;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Enums;
using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JuniorOnly.Application.Services
{
    public class CandidateProfileService : ICandidateProfileService
    {
        private readonly ICandidateProfileRepository _profileRepository;

        public CandidateProfileService(ICandidateProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<CandidateProfileDto> CreateProfileAsync(CandidateProfileCreateDto profileDto)
        {
            var profile = profileDto.ToEntity();
            profile.Id = Guid.NewGuid();

            await _profileRepository.AddProfileAsync(profile);

            return profile.ToDto();
        }

        public async Task DeleteProfileAsync(Guid profileId)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(profileId);
            if (profile == null)
            {
                throw new NotFoundException($"Profile with ID {profileId} not found");
            }

            await _profileRepository.DeleteProfileAsync(profile);
        }

        public async Task<List<CandidateProfileDto>> GetAllProfilesAsync()
        {
            var profiles = await _profileRepository.GetAllProfilesAsync();

            return profiles.Select(p => p.ToDto()).ToList();
        }

        public async Task<CandidateProfileDto> GetProfileByIdAsync(Guid profileId)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(profileId);
            if (profile == null)
            {
                throw new NotFoundException($"Profile with ID {profileId} not found");
            }

            return profile.ToDto();
        }

        public async Task<CandidateProfileDto> GetProfileByUserIdAsync(Guid userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);
            if (profile == null)
            {
                throw new NotFoundException($"Profile with user ID {userId.ToString()} not found");
            }

            return profile.ToDto();
        }

        public Task<List<CandidateProfileDto>> SearchProfilesAsync(string? location = null, ExperienceLevel? experienceLevel = null)
        {
            throw new NotImplementedException();
        }

        public async Task<CandidateProfileDto> UpdateProfileAsync(Guid profileId, CandidateProfileUpdateDto profileDto)
        {
            var profile = await _profileRepository.GetProfileByIdAsync(profileId);
            if (profile == null)
            {
                throw new NotFoundException($"Profile with ID {profileId} not found");
            }

            profile.UpdateFrom(profileDto);

            await _profileRepository.SaveChangesAsync();

            return profile.ToDto();
        }
    }
}
