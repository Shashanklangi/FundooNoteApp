﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(LabelModel labelModel);
        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID);
    }
}
