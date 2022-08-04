using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        public readonly FundooContext fundooContext;

        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollaboratorEntity AddCollab(CollabModel collabModel)
        {
            try
            {
                var userData = fundooContext.UserTable.Where(r => r.Email == collabModel.CollabEmail).FirstOrDefault();
                var noteData = fundooContext.NotesTable.Where(r => r.NoteID == collabModel.NoteID).FirstOrDefault();
                if (userData != null && noteData != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity()
                    {
                        CollabEmail = collabModel.CollabEmail,
                        NoteID = collabModel.NoteID,
                        UserId = userData.UserId
                    };
                    fundooContext.CollaboratorTable.Add(collaboratorEntity);
                    fundooContext.SaveChanges();
                    return collaboratorEntity;
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
    }
}
