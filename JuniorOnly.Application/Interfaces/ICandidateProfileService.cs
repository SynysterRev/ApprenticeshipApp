using JuniorOnly.Application.DTO.CandidateProfile;
using JuniorOnly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Interfaces
{
    public interface ICandidateProfileService
    {
        /// <summary>
        /// Gets all candidate profiles.
        /// </summary>
        /// <returns>List of CandidateProfileDto representing all profiles.</returns>
        public Task<List<CandidateProfileDto>> GetAllProfilesAsync();

        /// <summary>
        /// Gets a candidate profile by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the candidate profile.</param>
        /// <returns>CandidateProfileDto if found, otherwise null.</returns>
        public Task<CandidateProfileDto?> GetProfileByIdAsync(Guid id);

        /// <summary>
        /// Gets a candidate profile by the user's unique identifier.
        /// </summary>
        /// <param name="userId">Guid of the user.</param>
        /// <returns>CandidateProfileDto if found, otherwise null.</returns>
        public Task<CandidateProfileDto?> GetProfileByUserIdAsync(Guid userId);

        /// <summary>
        /// Creates a new candidate profile.
        /// </summary>
        /// <param name="profileDto">CandidateProfileCreateDto containing profile data.</param>
        /// <returns>The created CandidateProfileDto.</returns>
        public Task<CandidateProfileDto> CreateProfileAsync(CandidateProfileCreateDto profileDto);

        /// <summary>
        /// Updates an existing candidate profile.
        /// </summary>
        /// <param name="id">Guid of the profile to update.</param>
        /// <param name="profileDto">CandidateProfileUpdateDto with updated data.</param>
        /// <returns>The updated CandidateProfileDto if found, otherwise null.</returns>
        public Task<CandidateProfileDto?> UpdateProfileAsync(Guid id, CandidateProfileUpdateDto profileDto);

        /// <summary>
        /// Deletes a candidate profile.
        /// </summary>
        /// <param name="id">Guid of the profile to delete.</param>
        /// <returns>True if deleted, false otherwise.</returns>
        public Task<bool> DeleteProfileAsync(Guid id);

        /// <summary>
        /// Searches candidate profiles by location or experience level.
        /// </summary>
        /// <param name="location">Optional location filter.</param>
        /// <param name="experienceLevel">Optional experience level filter.</param>
        /// <returns>List of matching CandidateProfileDto.</returns>
        public Task<List<CandidateProfileDto>> SearchProfilesAsync(string? location = null, ExperienceLevel? experienceLevel = null);
    }
}