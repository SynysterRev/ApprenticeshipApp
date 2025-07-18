using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface ICandidateProfileRepository
    {
        /// <summary>
        /// Get all candidate profiles in the database
        /// </summary>
        /// <returns>A list of all candidate profiles</returns>
        public Task<List<CandidateProfile>> GetAllProfilesAsync();

        /// <summary>
        /// Get candidate profile by user ID
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>The candidate profile if found, null otherwise</returns>
        public Task<CandidateProfile?> GetProfileByUserIdAsync(Guid userId);

        /// <summary>
        /// Get candidate profile by profile ID
        /// </summary>
        /// <param name="profileId">The ID of the profile</param>
        /// <returns>The candidate profile if found, null otherwise</returns>
        public Task<CandidateProfile?> GetProfileByIdAsync(Guid profileId);

        /// <summary>
        /// Get all favorites for a candidate
        /// </summary>
        /// <param name="profileId">The ID of the candidate profile</param>
        /// <returns>List of favorites for the candidate</returns>
        public Task<List<Favorite>> GetFavoritesByProfileAsync(Guid profileId);

        /// <summary>
        /// Add a new candidate profile
        /// </summary>
        /// <param name="profile">The profile to add</param>
        /// <returns>The newly created profile</returns>
        public Task<CandidateProfile> AddProfileAsync(CandidateProfile profile);

        /// <summary>
        /// Update an existing candidate profile
        /// </summary>
        /// <param name="updatedProfile">The profile to update</param>
        /// <returns>The updated profile or null if not found</returns>
        public Task<CandidateProfile?> UpdateProfileAsync(CandidateProfile updatedProfile);

        /// <summary>
        /// Delete a candidate profile
        /// </summary>
        /// <param name="profileId">The ID of the profile to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        public Task<bool> DeleteProfileAsync(Guid profileId);

        /// <summary>
        /// Search profiles by experience level or desired job titles
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>List of matching profiles</returns>
        public Task<List<CandidateProfile>> SearchProfilesAsync(string searchTerm);
    }
}