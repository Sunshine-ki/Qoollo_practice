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
	
		public static List<User> GetUserById(int uId, IMongoDatabase db) //�������� ������ user
		{
			var filter = new BsonDocument("Id", uId.ToString() );
			var user = db.GetCollection<User>("Users").Find(filter).ToList();
			
			Console.WriteLine(user); //������� � �������
			return user;
		}
		public static List<User> GetAllUsers(IMongoDatabase db) //������� ���� user 
        {
			var filter = new BsonDocument("UId", new BsonDocument("$gt", 0));
			var users =  db.GetCollection<User>("Users").Find(filter).ToList();
			foreach (var doc in users)
			{
				Console.WriteLine(doc); //������� � �������
			}
			return users;
		}

		public static void SetUser(User u, IMongoDatabase db) //������ user � ���� ������
		{
			try
            {
				db.GetCollection<User>("Users").InsertOne(u); //������� ������ ���������  � �������� � ��� user
			}
            catch { Console.WriteLine("Same index Exception mb?\n"); }
			  
			Console.WriteLine("User ��� ��������!\n");
		}

		public static void UpdateUser(int userOldId, User userNew, IMongoDatabase db) // �������� ������ userID 
		{
			var filter = new BsonDocument("UId", userOldId); //��� ���� ������� �������� �� ������� ������ ����� � ��, ��������
			var update = Builders<User>.Update.Set("UId", userNew.UId)
											  .Set("Name", userNew.Name)
											  .Set("Surname", userNew.Surname)
											  .Set("Age", userNew.Age);
			var user = db.GetCollection<User>("Users").UpdateOne(filter, update);

			Console.WriteLine("User ��� ��������!\n");
		}

		public static void DeleteUserById(int UId, IMongoDatabase db) // ������� user �� ��
		{
			var filter = Builders<User>.Filter.Eq("UId", UId);
			var users = db.GetCollection<User>("Users").DeleteOne(filter);
			Console.WriteLine("User ������ �� ��!\n");
		}
	}
}