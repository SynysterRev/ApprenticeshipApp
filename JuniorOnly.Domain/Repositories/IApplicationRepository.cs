using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface IApplicationRepository
    {
        /// <summary>
        /// Get all applications in the database
        /// </summary>
        /// <returns>A list of all applications</returns>
        public Task<List<Application>> GetAllApplicationsAsync();

        /// <summary>
        /// Get all applications for a specific candidate
        /// </summary>
        /// <param name="candidateId">The ID of the candidate</param>
        /// <returns>A list of all applications for the candidate</returns>
        public Task<List<Application>> GetApplicationsByCandidateAsync(Guid candidateId);

        /// <summary>
        /// Get all applications for a specific job offer
        /// </summary>
        /// <param name="offerId">The ID of the job offer</param>
        /// <returns>A list of all applications for the job offer</returns>
        public Task<List<Application>> GetApplicationsByOfferAsync(Guid offerId);

        /// <summary>
        /// Get application by its ID
        /// </summary>
        /// <param name="applicationId">The application id</param>
        /// <returns>The application if found, null otherwise</returns>
        public Task<Application?> GetApplicationByIdAsync(Guid applicationId);

        /// <summary>
        /// Add a new application in the database
        /// </summary>
        /// <param name="application">The application to add</param>
        /// <returns>The new application added</returns>
        public Task<Application> AddApplicationAsync(Application application);

        /// <summary>
        /// Update the application status or details
        /// </summary>
        /// <param name="updatedApplication">The application to update</param>
        /// <returns>The updated application or null if not found</returns>
        public Task<Application?> UpdateApplicationAsync(Application updatedApplication);

        /// <summary>
        /// Delete an application
        /// </summary>
        /// <param name="applicationId">The ID of the application to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        public Task<bool> DeleteApplicationAsync(Guid applicationId);
    }
}