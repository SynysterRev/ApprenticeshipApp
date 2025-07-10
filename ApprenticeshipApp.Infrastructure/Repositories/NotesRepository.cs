using ApprenticeshipApp.Domain.Entities;
using ApprenticeshipApp.Domain.Repositories;
using ApprenticeshipApp.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ApprenticeshipApp.Infrastructure.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NotesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Note> AddNoteAsync(Note newNote)
        {
            _dbContext.Notes.Add(newNote);
            await _dbContext.SaveChangesAsync();

            return newNote;
        }

        public async Task<bool> DeleteNoteAsync(Guid noteId)
        {
            Note? foundNote = await _dbContext.Notes.FindAsync(noteId);
            if (foundNote == null)
            {
                return false;
            }
            _dbContext.Notes.Remove(foundNote);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _dbContext.Notes.ToListAsync();
        }

        public async Task<List<Note>> GetAllNotesByApprenticeAsync(Guid apprenticeGuid)
        {
            return await _dbContext.Notes.Where(n => n.UserId == apprenticeGuid).ToListAsync();
        }

        public async Task<List<Note>> GetAllNotesBySessionAsync(Guid sessionGuid)
        {
            return await _dbContext.Notes.Where(n => n.SessionId == sessionGuid).ToListAsync();
        }

        public async Task<Note?> GetNoteByGuidAsync(Guid noteId)
        {
            return await _dbContext.Notes.FindAsync(noteId);
        }

        public async Task<Note?> UpdateNoteAsync(Note updatedNote)
        {
            Note? foundNote = await _dbContext.Notes.FindAsync(updatedNote.Id);
            if (foundNote == null)
            {
                return null;
            }
            _dbContext.Notes.Entry(foundNote).CurrentValues.SetValues(updatedNote);
            await _dbContext.SaveChangesAsync();
            return foundNote;
        }
    }
}
