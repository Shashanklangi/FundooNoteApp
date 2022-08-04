using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollaboratorEntity AddCollab(CollabModel collabModel);
        public string RemoveCollab(long collabID, long userId);
    }
}
