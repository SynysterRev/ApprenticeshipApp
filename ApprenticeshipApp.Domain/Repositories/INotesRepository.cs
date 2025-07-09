using ApprenticeshipApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprenticeshipApp.Domain.Repositories
{
    public interface INotesRepository
    {
        /// <summary>
        /// Get all notes stocked in the database
        /// </summary>
        /// <returns>A list of all notes</returns>
        public Task<List<Note>> GetAllNotes();

        /// <summary>
        /// Get all notes related to an apprentice
        /// </summary>
        /// <param name="apprenticeGuid">The ID of the apprentice</param>
        /// <returns>A list of all the notes of an apprentice</returns>
        public Task<List<Note>> GetAllNotesByApprentice(Guid apprenticeGuid);

        /// <summary>
        /// Get all notes related to an session
        /// </summary>
        /// <param name="sessionGuid">The ID of the session</param>
        /// <returns>A list of all the notes of an session</returns>
        public Task<List<Note>> GetAllNotesBySession(Guid sessionGuid);

        /// <summary>
        /// Get note matching with ID if one
        /// </summary>
        /// <param name="noteId">The guid of the wanted note</param>
        /// <returns>The note if any matching</returns>
        public Task<Note?> GetNoteByGuid(Guid noteId);

        /// <summary>
        /// Add a new note in the database
        /// </summary>
        /// <param name="newNote">The new note to add</param>
        /// <returns>The added note</returns>
        public Task<Note> AddNote(Note newNote);

        /// <summary>
        /// Update the note with the same ID
        /// </summary>
        /// <param name="updatedNote">The updated note</param>
        /// <returns>The updated note</returns>
        public Task<Note> UpdateNote(Note updatedNote);

        /// <summary>
        /// Delete the note matching with the ID
        /// </summary>
        /// <param name="noteId">The ID of the note to delete</param>
        /// <returns>True if delete, false otherwise</returns>
        public Task<bool> DeleteNote(Guid noteId);
    }
}
