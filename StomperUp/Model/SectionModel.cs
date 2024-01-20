using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class SectionModel
    {
        public ObjectId _id { get; set; }
        public string nameSection { get; set; }
        public string idLesson { get; set; }
        public string material { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
