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

		public List<User> GetUserById(int uId) //�������� ������ user
		{
			var filter = new BsonDocument("Id", uId.ToString());
			var user = _db.GetCollection<User>("Users").Find(filter).ToList();

			Console.WriteLine(user); //������� � �������
			return user;
		}

		public List<User> GetAllUsers() //������� ���� user 
		{
			var filter = new BsonDocument("UId", new BsonDocument("$gt", 0));
			var users = _db.GetCollection<User>("Users").Find(filter).ToList();
			foreach (var doc in users)
			{
				Console.WriteLine(doc); //������� � �������
			}
			return users;
		}

		public void SetUser(User u) //������ user � ���� ������
		{
			try
			{
				_db.GetCollection<User>("Users").InsertOne(u); //������� ������ ���������  � �������� � ��� user
			}
			catch { Console.WriteLine("Same index Exception mb?\n"); }

			Console.WriteLine("User ��� ��������!\n");
		}

		public void UpdateUser(int userOldId, User userNew) // �������� ������ userID 
		{
			var filter = new BsonDocument("UId", userOldId); //��� ���� ������� �������� �� ������� ������ ����� � ��, ��������
			var update = Builders<User>.Update.Set("UId", userNew.UId)
											  .Set("Name", userNew.Name)
											  .Set("Surname", userNew.Surname)
											  .Set("Age", userNew.Age);
			var user = _db.GetCollection<User>("Users").UpdateOne(filter, update);

			Console.WriteLine("User ��� ��������!\n");
		}

		public void DeleteUserById(int UId) // ������� user �� ��
		{
			var filter = Builders<User>.Filter.Eq("UId", UId);
			var users = _db.GetCollection<User>("Users").DeleteOne(filter);
			Console.WriteLine("User ������ �� ��!\n");
		}
	}
}
