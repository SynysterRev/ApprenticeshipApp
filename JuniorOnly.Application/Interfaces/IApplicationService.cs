using JuniorOnly.Application.DTO.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuniorOnly.Application.Interfaces
{
    public interface IApplicationService
    {
        /// <summary>
        /// Gets all applications.
        /// </summary>
        /// <returns>List of ApplicationDto representing all applications.</returns>
        public Task<List<ApplicationDto>> GetAllApplicationsAsync();

        /// <summary>
        /// Gets an application by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the application.</param>
        /// <returns>ApplicationDto if found, otherwise null.</returns>
        public Task<ApplicationDto?> GetApplicationByIdAsync(Guid id);

        /// <summary>
        /// Gets all applications for a specific candidate.
        /// </summary>
        /// <param name="candidateId">Guid of the candidate.</param>
        /// <returns>List of ApplicationDto for the candidate.</returns>
        public Task<List<ApplicationDto>> GetApplicationsByCandidateAsync(Guid candidateId);

        /// <summary>
        /// Gets all applications for a specific job offer.
        /// </summary>
        /// <param name="offerId">Guid of the job offer.</param>
        /// <returns>List of ApplicationDto for the offer.</returns>
        public Task<List<ApplicationDto>> GetApplicationsByOfferAsync(Guid offerId);

        /// <summary>
        /// Creates a new application.
        /// </summary>
        /// <param name="applicationDto">ApplicationCreateDto containing application data.</param>
        /// <returns>The created ApplicationDto.</returns>
        public Task<ApplicationDto> CreateApplicationAsync(ApplicationCreateDto applicationDto);

        /// <summary>
        /// Updates an existing application.
        /// </summary>
        /// <param name="id">Guid of the application to update.</param>
        /// <param name="applicationDto">ApplicationUpdateDto with updated data.</param>
        /// <returns>The updated ApplicationDto if found, otherwise null.</returns>
        public Task<ApplicationDto?> UpdateApplicationAsync(Guid id, ApplicationUpdateDto applicationDto);

        /// <summary>
        /// Deletes an application.
        /// </summary>
        /// <param name="id">Guid of the application to delete.</param>
        /// <returns>True if deleted, false otherwise.</returns>
        public Task<bool> DeleteApplicationAsync(Guid id);
    }
}