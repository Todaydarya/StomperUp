using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class CourseModel
    {
        public ObjectId _id { get; set; }
        public string nameCourse { get; set; }
        public string programmingLanguage { get; set; }
        public int coin { get; set; }
        public int countLesson { get; set; }
    }
}
