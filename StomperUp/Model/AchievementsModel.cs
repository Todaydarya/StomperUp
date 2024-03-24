using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class AchievementsModel
    {
        public ObjectId _id { get; set; }
        public string nameAchievement { get; set; }
        public int coin { get; set; }
        public byte[] picturePath { get; set; }
        public string description { get; set; }
    }
}
