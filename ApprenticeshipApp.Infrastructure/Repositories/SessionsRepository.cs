using ApprenticeshipApp.Domain.Entities;
using ApprenticeshipApp.Domain.Repositories;
using ApprenticeshipApp.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ApprenticeshipApp.Infrastructure.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Session> AddSessionAsync(Session newSession)
        {
            _dbContext.Sessions.Add(newSession);

            await _dbContext.SaveChangesAsync();

            return newSession;
        }

        public async Task<Inscription> AddInscriptionAsync(Inscription inscription)
        {
            _dbContext.Inscriptions.Add(inscription);
            await _dbContext.SaveChangesAsync();

            return inscription;
        }

        public async Task<bool> DeleteSessionAsync(Guid sessionId)
        {
            Session? foundSession = await _dbContext.Sessions.FindAsync(sessionId);

            if (foundSession == null)
            {
                return false;
            }

            _dbContext.Sessions.Remove(foundSession);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return await _dbContext.Sessions.Include(s => s.Course).OrderByDescending(s => s.StartTime).ToListAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(Guid sessionId)
        {
            return await _dbContext.Sessions.Include(s => s.Course).FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public async Task<List<Session>> GetSessionsByCourseAsync(Guid courseId)
        {
            return await _dbContext.Sessions
                .Where(s => s.CourseId == courseId)
                .Include(s => s.Course)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<Session?> UpdateSessionAsync(Session updatedSession)
        {
            Session? foundSession = await _dbContext.Sessions.FindAsync(updatedSession.Id);
            if (foundSession == null)
            {
                return null;
            }
            _dbContext.Sessions.Entry(foundSession).CurrentValues.SetValues(updatedSession);
            await _dbContext.SaveChangesAsync();

            return foundSession;
        }
    }
}
