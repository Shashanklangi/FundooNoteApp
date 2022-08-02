using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNote(long UserId, NotesModel notesModel);
        public NotesEntity UpdateNote(NotesModel notesModel, long NoteId);
    }
}
