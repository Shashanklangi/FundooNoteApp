using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {

        public NotesEntity CreateNote(long UserId, NotesModel notesModel);
        public NotesEntity UpdateNote(NotesModel notesModel, long NoteId);
        public NotesEntity DeleteNotes(long NoteId);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public bool Pinned(long NoteID, long userId);
        public bool Trashed(long NoteID, long userId);
        public bool Archieve(long NoteID, long userId);
        public NotesEntity ColourNote(long NoteId, string color);
        public string Imaged(long NoteID, long userId, IFormFile image);

    }
}
