﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class LessonsModel
    {
        public ObjectId _id { get; set; }
        public string nameLessons { get; set; }
        public int idCourse { get; set; }
        public string idTest { get; set; }
        public string material { get; set; }
    }
}
