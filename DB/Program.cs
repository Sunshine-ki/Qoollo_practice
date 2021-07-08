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
			User u = new User(0, "Alis");
			Facade f = new Facade();
			f.SetUser(u);


			SaveDocs().GetAwaiter().GetResult();
		}

		private static async Task SaveDocs()
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
		}
	}
}