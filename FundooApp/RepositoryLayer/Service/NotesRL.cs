using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public NotesRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public NotesEntity CreateNote(long UserId, NotesModel notesModel)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = notesModel.Title;
                notesEntity.Description = notesModel.Description;
                notesEntity.Reminder = notesModel.Reminder;
                notesEntity.Colour = notesModel.Colour;
                notesEntity.Image = notesModel.Image;
                notesEntity.Archieve = notesModel.Archieve;
                notesEntity.Pin = notesModel.Pin;
                notesEntity.Trash = notesModel.Trash;
                notesEntity.Created = notesModel.Created;
                notesEntity.Modify = notesModel.Modify;
                notesEntity.UserId = UserId;
                fundooContext.NotesTable.Add(notesEntity);
                int result = fundooContext.SaveChanges();

                if (result != 0)
                {
                    return notesEntity;
                }
                else
                {
                    return null;
                }
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
                var update = fundooContext.NotesTable.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.Title = notesModel.Title;
                    update.Description = notesModel.Description;
                    update.Reminder = notesModel.Reminder;
                    update.Colour = notesModel.Colour;
                    update.Image = notesModel.Image;
                    update.Pin = notesModel.Pin;
                    update.Archieve = notesModel.Archieve;
                    update.Trash = notesModel.Trash;
                    fundooContext.NotesTable.Update(update);
                    fundooContext.SaveChanges();
                    return update;
                }
                else
                {
                    return null;
                }
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
                var deleteNote = fundooContext.NotesTable.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (deleteNote != null)
                {
                    fundooContext.NotesTable.Remove(deleteNote);
                    fundooContext.SaveChanges();
                    return deleteNote;
                }
                else
                {
                    return null;
                }
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
                var result = fundooContext.NotesTable.Where(id => id.UserId == userId);
                return result;
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
                var result = fundooContext.NotesTable.Where(r => r.UserId == userId && r.NoteID == NoteID).FirstOrDefault();

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }

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
                var result = fundooContext.NotesTable.Where(r => r.UserId == userId && r.NoteID == NoteID).FirstOrDefault();

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }

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
                var result = fundooContext.NotesTable.Where(r => r.UserId == userId && r.NoteID == NoteID).FirstOrDefault();

                if (result.Archieve == true)
                {
                    result.Archieve = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity ColourNote(long NoteId, string color)
        {
            var result = fundooContext.NotesTable.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {
                if (color != null)
                {
                    result.Colour = color;
                    fundooContext.NotesTable.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public string Imaged(long NoteID, long userId, IFormFile image)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(this.configuration["CloudinaryAccount:CloudName"], this.configuration["CloudinaryAccount:APIKey"], this.configuration["CloudinaryAccount:APISecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image Upload Successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
