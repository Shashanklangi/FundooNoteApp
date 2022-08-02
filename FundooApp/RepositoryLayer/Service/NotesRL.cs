using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
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

            catch (Exception e)

            {
                throw;
            }
        }
    }
}
