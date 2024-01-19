using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StomperUp.Model
{
    internal class UserModel
    {
        public ObjectId _id { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public string middleName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string picturePath { get; set; }
        public string role { get; set; }
        public string activeCourses { get; set; }
        public string receivedAchievements { get; set; }
        public int coin { get; set; }
    }
}
