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
			string connectionString = "mongodb://localhost:27017";
			MongoClient client = new MongoClient(connectionString);

			// IMongoDatabase database = client.GetDatabase("test");

			Console.WriteLine("Hello World!");
		}
	}
}