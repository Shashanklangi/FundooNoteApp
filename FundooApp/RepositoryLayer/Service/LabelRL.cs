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
    public class LabelRL : ILabelRL
    {
        public readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity CreateLabel(LabelModel labelModel)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(r => r.NoteID == labelModel.NoteID).FirstOrDefault();
                if (result != null)
                {
                    LabelEntity label = new LabelEntity();
                    label.LabelName = labelModel.LabelName;
                    label.NoteID = result.NoteID;
                    label.UserId = result.UserId;

                    fundooContext.LabelTable.Add(label);
                    fundooContext.SaveChanges();
                    return label;
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
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                var update = fundooContext.LabelTable.Where(r => r.LabelID == labelID).FirstOrDefault();
                if (update != null && update.LabelID == labelID)
                {
                    update.LabelName = labelModel.LabelName;
                    update.NoteID = labelModel.NoteID;

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
        public LabelEntity DeleteLabel(long labelID, long userId)
        {
            try
            {
                var deleteLabel = fundooContext.LabelTable.Where(r => r.LabelID == labelID).FirstOrDefault();
                if (deleteLabel != null)
                {
                    fundooContext.LabelTable.Remove(deleteLabel);
                    fundooContext.SaveChanges();
                    return deleteLabel;
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
        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                var result = fundooContext.LabelTable.ToList().Where(x => x.UserId == userId);
                if (result != null)
                {
                    return result;
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
