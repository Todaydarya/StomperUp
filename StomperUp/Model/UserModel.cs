using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

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

        public string fullName
        {
            get
            {
                return $"{surName} {firstName} {middleName}";
            }
            set
            {
                string[] parts = value.Split(' ');
                if (parts.Length >= 3)
                {
                    surName = parts[0];
                    firstName = parts[1];
                    middleName = parts[2];
                    OnPropertyChanged(nameof(fullName));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
