using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollaboratorEntity AddCollab(CollabModel collabModel)
        {
            try
            {
                return collabRL.AddCollab(collabModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string RemoveCollab(long collabID, long userId)
        {
            try
            {
                return collabRL.RemoveCollab(collabID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollaboratorEntity> GetCollab(long noteId, long userId)
        {
            try
            {
                return collabRL.GetCollab(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
