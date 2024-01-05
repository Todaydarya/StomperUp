using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomperUp.Model
{
    internal class UserModel
    {
        public static List<UserModel> Users { get; } = new List<UserModel>
        {
            new UserModel
            {
                _id = ObjectId.GenerateNewId(),
                firstName = "John",
                surName = "Doe",
                email = "john.doe@example.com",
                password = "hashedPassword",
                picturePath = "/images/john_doe.jpg",
                role = "user",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
                __v = 1
            },
            // Добавьте других пользователей по аналогии
        };

        public ObjectId _id { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string picturePath { get; set; }
        public string role { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }

        // Метод для поиска пользователя по email
        public static UserModel GetUserByEmail(string email)
        {
            return Users.Find(u => u.email == email);
        }
    }
}
