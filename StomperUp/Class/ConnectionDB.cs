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
        private static IMongoCollection<T> Connect<T>(string collectionName)
        {
            string username = "todaydarya";
            string password = "P@ssw0rd";

            string encodedUsername = Uri.EscapeDataString(username);
            string encodedPassword = Uri.EscapeDataString(password);

            string connectionString = $"mongodb+srv://{encodedUsername}:{encodedPassword}@stomperup.xeqa9oe.mongodb.net/StomperUp?retryWrites=true&w=majority";

            MongoClient client = new MongoClient(connectionString);

            var database = client.GetDatabase("StomperUp");

            var collection = database.GetCollection<T>(collectionName);

            return collection;
        }

        private static IMongoCollection<UserModel> ConnectUser()
        {
            return Connect<UserModel>("users");
        }

        public static async Task<List<UserModel>> GetUsers()
        {
            var collection = ConnectUser();
            List<UserModel> users = await collection.Find(new BsonDocument()).ToListAsync();

            return users;
        }

        public static async Task AddUser(UserModel user)
        {
            var collection = ConnectUser();
            await collection.InsertOneAsync(user);
        }

        public static async Task UpdateUserPassword(string userId, UserModel updatedUser)
        {
            var collection = ConnectUser();
            var filter = Builders<UserModel>.Filter.Eq("_id", userId);
            var update = Builders<UserModel>.Update.Set(u => u.password, updatedUser.password);
            await collection.UpdateOneAsync(filter, update);
        }

        public static async Task UpdateUser(ObjectId userId, UserModel updatedUser)
        {
            var collection = ConnectUser();
            var filter = Builders<UserModel>.Filter.Eq("_id", userId);

            var update = Builders<UserModel>.Update
                .Set(u => u.phone, updatedUser.phone)
                .Set(u => u.firstName, updatedUser.firstName)
                .Set(u => u.surName, updatedUser.surName)
                .Set(u => u.middleName, updatedUser.middleName)
                .Set(u => u.coin, updatedUser.coin)
                .Set(u => u.role, updatedUser.role)
                .Set(u => u.email, updatedUser.email);

            await collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }
        private static IMongoCollection<TaskModel> ConnectTask()
        {
            return Connect<TaskModel>("task");
        }

        public static async Task<List<TaskModel>> GetTasks()
        {
            var collection = ConnectTask();
            List<TaskModel> tasks = await collection.Find(new BsonDocument()).ToListAsync();

            return tasks;
        }

        public static async Task AddTask(TaskModel tasks)
        {
            var collection = ConnectTask();
            await collection.InsertOneAsync(tasks);
        }

        public static async Task DeleteTaskById(ObjectId taskId)
        {
            var collection = ConnectTask();

            var filter = Builders<TaskModel>.Filter.Eq("_id", taskId);

            await collection.DeleteOneAsync(filter);
        }


        public static async Task UpdateTask(ObjectId taskId, TaskModel updatedTask)
        {
            var collection = ConnectTask();

            var filter = Builders<TaskModel>.Filter.Eq("_id", taskId);

            UpdateDefinition<TaskModel> update = Builders<TaskModel>.Update.Set("isActive", updatedTask.isActive);

            await collection.UpdateOneAsync(filter, update);
        }
        private static IMongoCollection<CourseModel> ConnectCourse()
        {
            return Connect<CourseModel>("course");
        }
        public static async Task<List<CourseModel>> GetCourse()
        {
            var collection = ConnectCourse();
            List<CourseModel> course = await collection.Find(new BsonDocument()).ToListAsync();

            return course;
        }
    }
}
