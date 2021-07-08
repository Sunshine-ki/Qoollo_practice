using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
// using User;

namespace DB
{
	public class Facade
	{
	
		public static List<User> GetUserById(int uId, IMongoDatabase db) //получить данные user
		{
			var filter = new BsonDocument("Id", uId.ToString() );
			var user = db.GetCollection<User>("Users").Find(filter).ToList();
			
			Console.WriteLine(user); //отладка в консоль
			return user;
		}
		public static List<User> GetAllUsers(IMongoDatabase db) //достать всех user 
        {
			var filter = new BsonDocument("UId", new BsonDocument("$gt", 0));
			var users =  db.GetCollection<User>("Users").Find(filter).ToList();
			foreach (var doc in users)
			{
				Console.WriteLine(doc); //отладка в консоль
			}
			return users;
		}

		public static void SetUser(User u, IMongoDatabase db) //кладет user в базу данных
		{
			try
            {
				db.GetCollection<User>("Users").InsertOne(u); //выбрали нужную коллекцию  и положили в нее user
			}
            catch { Console.WriteLine("Same index Exception mb?\n"); }
			  
			Console.WriteLine("User был добавлен!\n");
		}

		public static void UpdateUser(int userOldId, User userNew, IMongoDatabase db) // обновить данные userID 
		{
			var filter = new BsonDocument("UId", userOldId); //тут надо сделать проверку на наличие нового ключа в бд, наверное
			var update = Builders<User>.Update.Set("UId", userNew.UId)
											  .Set("Name", userNew.Name)
											  .Set("Surname", userNew.Surname)
											  .Set("Age", userNew.Age);
			var user = db.GetCollection<User>("Users").UpdateOne(filter, update);

			Console.WriteLine("User был Обновлен!\n");
		}

		public static void DeleteUserById(int UId, IMongoDatabase db) // удалить user из бд
		{
			var filter = Builders<User>.Filter.Eq("UId", UId);
			var users = db.GetCollection<User>("Users").DeleteOne(filter);
			Console.WriteLine("User удален из БД!\n");
		}
	}
}