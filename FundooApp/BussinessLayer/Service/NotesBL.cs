using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public NotesEntity DeleteNotes(long NoteId)
        {
            try
            {
                return notesRL.DeleteNotes(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                return notesRL.ReadNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Pinned(long NoteID, long userId)
        {
            try
            {
                return notesRL.Pinned(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trashed(long NoteID, long userId)
        {
            try
            {
                return notesRL.Trashed(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Archieve(long NoteID, long userId)
        {
            try
            {
                return notesRL.Archieve(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity ColourNote(long NoteId, string color)
        {
            try
            {
                return notesRL.ColourNote(NoteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Imaged(long NoteID, long userId, IFormFile image)
        {
            try
            {
                return notesRL.Imaged(NoteID, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
