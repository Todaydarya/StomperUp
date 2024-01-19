using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class TaskModel
    {
        public ObjectId _id { get; set; }
        public string _idUser { get; set; }
        public string taskName { get; set; }
        public bool isActive { get; set; }
        public string isPriority { get; set; }
        public DateTime remind { get; set; }
        public string replay { get; set; }
    }
}
