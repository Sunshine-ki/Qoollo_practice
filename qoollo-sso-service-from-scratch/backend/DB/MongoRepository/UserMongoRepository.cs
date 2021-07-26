using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using QoolloSSO.backend.DataBase.IRepository;
using QoolloSSO.backend.DataBase.Models;

namespace QoolloSSO.backend.DataBase.MongoRepository
{
	public class UserMongoRepository : IUserRepository
	{
		IMongoDatabase _db;
		public UserMongoRepository(IMongoDatabase db) { _db = db; }
		public UserMongoRepository(string connectionString, string databaseString)
		{
			var client = new MongoClient(connectionString);
			_db = client.GetDatabase(databaseString);
		}

		public async Task<User> GetByLogin(string login) //return user info using his id
		{
			var filter = Builders<User>.Filter.Eq("Login", login);
			var user = _db.GetCollection<User>("Users").Find(filter).ToList();

			if (user.Count > 0)
				return user[0];
			return new User();
		}

		public async Task<List<User>> GetAll() //return info about all users in DB
		{
			var filter = new BsonDocument();
			var users = _db.GetCollection<User>("Users").Find(filter).ToList();
			return users;
		}

		public async Task Set(User u) //create user in DB
		{
			try
			{
				_db.GetCollection<User>("Users").InsertOne(u); // set user in selected collection in DB
			}
			catch { Console.WriteLine("Same index Exception mb?\n"); }

			Console.WriteLine("User was added to DB!\n");
		}

		public async Task Update(string userOldId, User userNew) // update user info and searching him by userID 
		{
			var filter = Builders<User>.Filter.Eq("Id", userOldId); //filter that is used to find particular user
			var update = Builders<User>.Update.Set("Name", userNew.Name)
											.Set("Surname", userNew.Surname)
											.Set("Age", userNew.Age);
			var user = _db.GetCollection<User>("Users").UpdateOne(filter, update);

			Console.WriteLine("User info was updated!\n");
		}

		public async Task DeleteById(string Id) // delete user from DB
		{
			var filter = Builders<User>.Filter.Eq("Id", Id);
			var users = _db.GetCollection<User>("Users").DeleteOne(filter);
			Console.WriteLine("User was deleted!\n");
		}
	}
}
