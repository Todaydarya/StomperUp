using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class TestModel
    {
        public ObjectId _id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
