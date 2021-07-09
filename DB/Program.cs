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