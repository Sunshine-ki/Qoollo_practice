using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB
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

		public List<User> GetUserById(int uId) //return user info using his id
		{
			var filter = new BsonDocument("Id", uId.ToString());
			var user = _db.GetCollection<User>("Users").Find(filter).ToList();

			Console.WriteLine(user); //debug info
			return user;
		}

		public List<User> GetAllUsers() //return info about all users in DB
		{
			var filter = new BsonDocument("UId", new BsonDocument("$gt", 0));
			var users = _db.GetCollection<User>("Users").Find(filter).ToList();
			foreach (var doc in users)
			{
				Console.WriteLine(doc); //debug info
			}
			return users;
		}

		public void SetUser(User u) //create user in DB
		{
			try
			{
				_db.GetCollection<User>("Users").InsertOne(u); //get user in selected collection in DB
			}
			catch { Console.WriteLine("Same index Exception mb?\n"); }

			Console.WriteLine("User was added to DB!\n");
		}

		public void UpdateUser(int userOldId, User userNew) // update user info and searching him by userID 
		{
			var filter = new BsonDocument("UId", userOldId); //filter that is used to find particular user
			var update = Builders<User>.Update.Set("UId", userNew.UId)
											  .Set("Name", userNew.Name)
											  .Set("Surname", userNew.Surname)
											  .Set("Age", userNew.Age);
			var user = _db.GetCollection<User>("Users").UpdateOne(filter, update);

			Console.WriteLine("User info was updated!\n");
		}

		public void DeleteUserById(int UId) // delete user from DB
		{
			var filter = Builders<User>.Filter.Eq("UId", UId);
			var users = _db.GetCollection<User>("Users").DeleteOne(filter);
			Console.WriteLine("User was deleted!\n");
		}
	}
}
