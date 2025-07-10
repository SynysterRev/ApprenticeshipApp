using ApprenticeshipApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Domain.Repositories
{
    public interface ISessionsRepository
    {
        /// <summary>
        /// Get all sessions in the database
        /// </summary>
        /// <returns>A list of all sessions</returns>
        public Task<List<Session>> GetAllSessionsAsync();

        /// <summary>
        /// Get all sessions related to a course
        /// </summary>
        /// <param name="courseId">The ID of the course</param>
        /// <returns>A list of all sessions for a course</returns>
        public Task<List<Session>> GetSessionsByCourseAsync(Guid courseId);

        /// <summary>
        /// Get the session matching the ID if any
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <returns>A session if any matching</returns>
        public Task<Session?> GetSessionByIdAsync(Guid sessionId);

        /// <summary>
        /// Add a new session in the database
        /// </summary>
        /// <param name="session">The session to add</param>
        /// <returns>The new session added</returns>
        public Task<Session> AddSessionAsync(Session newSession);

        /// <summary>
        /// Add a new inscription
        /// </summary>
        /// <param name="inscription">The inscription to add</param>
        /// <returns>The new inscription added</returns>
        public Task<Inscription> AddInscriptionAsync(Inscription inscription);

        /// <summary>
        /// Update the session with the same ID
        /// </summary>
        /// <param name="updatedSession">The session to update</param>
        /// <returns>The updated session or null if the course isn't found</returns>
        public Task<Session?> UpdateSessionAsync(Session updatedSession);

        /// <summary>
        /// Delete the session with the matching ID
        /// </summary>
        /// <param name="sessionId">The ID of the session to delete</param>
        /// <returns>True if delete, false otherwise</returns>
        public Task<bool> DeleteSessionAsync(Guid sessionId);
    }
}
