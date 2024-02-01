using MongoDB.Bson;
using MongoDB.Driver;
using StomperUp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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

            // Убедитесь, что picturePaths инициализирован как пустой массив
            if (user.picturePaths == null)
            {
                user.picturePaths = new List<byte[]>();
            }

            await collection.InsertOneAsync(user);
        }


        public static async Task UpdateUserPassword(ObjectId userId, UserModel updatedUser)
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
        public static async Task AddImage(ObjectId userId, byte[] imageData)
        {
            var collection = ConnectUser();
            var filter = Builders<UserModel>.Filter.Eq("_id", userId);
            var update = Builders<UserModel>.Update.Push(u => u.picturePaths, imageData);
            await collection.UpdateOneAsync(filter, update);
        }

        public static async Task<List<byte[]>> GetUserImages(ObjectId userId)
        {
            var collection = ConnectUser();
            var filter = Builders<UserModel>.Filter.Eq("_id", userId);
            var result = await collection.Find(filter).FirstOrDefaultAsync();

            return result?.picturePaths ?? new List<byte[]>();
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
        public static async Task AddCourse(CourseModel course)
        {
            var collection = ConnectCourse();
            await collection.InsertOneAsync(course);
        }

        private static IMongoCollection<LessonsModel> ConnectLesson()
        {
            return Connect<LessonsModel>("lesson");
        }
        public static async Task<List<LessonsModel>> GetLesson()
        {
            var collection = ConnectLesson();
            List<LessonsModel> lesson = await collection.Find(new BsonDocument()).ToListAsync();

            return lesson;
        }
        public static async Task AddLesson(LessonsModel lesson)
        {
            var collection = ConnectLesson();
            await collection.InsertOneAsync(lesson);
        }

        public static async Task<List<Tuple<CourseModel, List<LessonsModel>>>> GetCoursesWithLessons()
        {
            var coursesCollection = ConnectCourse();
            var lessonsCollection = ConnectLesson();

            var courses = await coursesCollection.Find(_ => true).ToListAsync();

            var result = courses.Select(course =>
            {
                var lessons = lessonsCollection.Find(l => l.idCourse == course._id).ToList();
                return Tuple.Create(course, lessons);
            }).ToList();

            return result;
        }

    }
}
