using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;

        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesEntity CreateNote(long UserId, NotesModel notesModel)
        {
            try
            {
                return notesRL.CreateNote(UserId, notesModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity UpdateNote(NotesModel notesModel, long NoteId)
        {
            try
            {
                return notesRL.UpdateNote(notesModel, NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
