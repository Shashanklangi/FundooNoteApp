using CommonLayer.Model;
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

    }
}
