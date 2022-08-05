using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(LabelModel labelModel);
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID);
    }
}
