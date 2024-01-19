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

        #region User
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
        #endregion

        #region Task
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

        ///Что за редактирование???
        public static async Task UpdateTask(ObjectId taskId, TaskModel updatedTask)
        {
            var collection = ConnectTask();

            var filter = Builders<TaskModel>.Filter.Eq("_id", taskId);

            var update = Builders<TaskModel>.Update.Set("isActive", updatedTask.isActive).CurrentDate("updatedAt");

            await collection.UpdateOneAsync(filter, update);
        }
        #endregion


        #region Курсы
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
        #endregion
    }
}
