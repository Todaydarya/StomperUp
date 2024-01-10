using MongoDB.Bson;
using MongoDB.Driver;
using StomperUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace StomperUp.Class
    {
        internal class ConnectionDB
        {
            public static async Task<List<UserModel>> GetUsers()
            {
                string connectionString = "mongodb+srv://root:root@cluster0.lwwaurf.mongodb.net/StomperTransit?retryWrites=true&w=majority";
                MongoClient client = new MongoClient(connectionString);

                var db = client.GetDatabase("Stomper");

                var collection = db.GetCollection<UserModel>("users");

                List<UserModel> users = await collection.Find(new BsonDocument()).ToListAsync();

                return users;
            }
        }
    }
