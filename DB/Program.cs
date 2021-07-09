using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace DB
{
	class Program
	{
		static void Main(string[] args)
		{
			IUserRepository repositoryUser = new UserMongoRepository("mongodb://localhost:27017", "Qoolloo_name");

			User u = new User()
			{
				Id = 4,
				UId = 4,
				Name = "name",
				Surname = "surname",
				Age = 20
			};

			repositoryUser.SetUser(u);
			// User user1 = new User(0, "Dan", "Danov", 228);
			// User user2 = new User(1, "Bill", "Billov", 117);

			// Facade.SetUser(user1, db); //готово

			//Facade.SetUser(user2, db);  //готово

			//Facade.UpdateUser(user1.UId, user2, db); //готово

			//Facade.GetAllUsers(db);  //готово

			//Facade.DeleteUserById(1, db); //готово

			//SaveDocs().GetAwaiter().GetResult();
		}

		public static IMongoDatabase MangoMakeConnection() //создаем соединение и возвращаем базу данных
		{
			string connectionString = "mongodb://localhost:27017";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("Qoolloo_name"); //подключились к базе test
			return database;

		}
		/*private static async Task SaveDocs() // это в принципе можно удалить но я пока оставил это сдесь
		{
			string connectionString = "mongodb://localhost:27017";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("test");
			var collection = database.GetCollection<BsonDocument>("people");
			BsonDocument person1 = new BsonDocument
			{
				{"Name", "Bill"},
				{"Age", 32},
				{"Languages", new BsonArray{"english", "german"}}
			};
			await collection.InsertOneAsync(person1);
		}*/
	}
}