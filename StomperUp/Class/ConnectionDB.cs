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
        private static IMongoCollection<UserModel> Connect()
        {
            string connectionString = "mongodb+srv://root:root@cluster0.lwwaurf.mongodb.net/StomperTransit?retryWrites=true&w=majority";
            MongoClient client = new MongoClient(connectionString);

            var database = client.GetDatabase("Stomper");

            var collection = database.GetCollection<UserModel>("users");

            return collection;
        }

        public static async Task<List<UserModel>> GetUsers()
        {
            var collection = Connect();
            List<UserModel> users = await collection.Find(new BsonDocument()).ToListAsync();

            return users;
        }

        public static async Task AddUser(UserModel user)
        {
            var collection = Connect();
            await collection.InsertOneAsync(user);
        }

        public static async Task<List<UserModel>> GetAllUsers()
        {
            var collection = Connect();
            List<UserModel> users = await collection.Find(new BsonDocument()).ToListAsync();

            return users;
        }
    }
}
