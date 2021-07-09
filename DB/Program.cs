using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using System.Text.Json;
using System.IO;
using System.Xml;


namespace DB
{
	class XMLReading //читает конфиг из xml
    {
		public static List<string> ReadXml(string fileName)
		{
			var DBinfo = new List<string>();
			using (XmlTextReader reader = new XmlTextReader(fileName))
			{
				while (reader.Read())
				{
					if (reader.IsStartElement("DB") && !reader.IsEmptyElement)
					{
						while (reader.Read())
						{
							if (reader.IsStartElement("ConnectionString"))
								DBinfo.Add(reader.ReadString());
							else if (reader.IsStartElement("DBName"))
								DBinfo.Add(reader.ReadString());
							else break;
						}
					}
				}
			}
			return DBinfo;
		}
	}
	

	class Program
	{
		static void Main(string[] args)
		{
			var DBinfo = XMLReading.ReadXml("C:/Users/maste/source/repos/Qoolo_practice/DB/DBconfig.xml");
			
			IUserRepository repositoryUser = new UserMongoRepository(DBinfo[0], DBinfo[1]);

            User u1 = new User()
            {
                Id = "4",
                Name = "name1",
                Surname = "surname1",
                Age = 20
            };

			User u2 = new User()
			{
				Id = "8",
				Name = "name2",
				Surname = "surname2",
				Age = 33
			};

			//repositoryUser.SetUser(u1);
			//repositoryUser.SetUser(u2);

			//repositoryUser.DeleteUserById(u1.Id);

			//repositoryUser.GetUserById(u1.Id);

			//repositoryUser.GetAllUsers();

			//repositoryUser.UpdateUser(u1.Id, u2);

		}

		public static IMongoDatabase MangoMakeConnection() //making connection to DB
		{
			string connectionString = "mongodb://localhost:27017";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("Qoolloo_name"); //Conneting to test DB
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