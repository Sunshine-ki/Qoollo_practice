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

using QoolloSSO.backend.DataBase.IRepository;
using QoolloSSO.backend.DataBase.MongoRepository;

namespace QoolloSSO.backend.DataBase.Models
{
	class Program
	{
		static void Main(string[] args)
		{
			// IUserRepository repositoryUser = new UserMongoRepository("mongodb://localhost:27017", "Qoollo_name");

			// repositoryUser.Set(new User() { Name = "Name1", Surname = "Surname1", Age = 18, Login = "Danya2" });
			// Console.WriteLine($"{u.Id} {u.Name} {u.Age}");

			// User u = Task.Run(() => repositoryUser.GetByLogin("Danya")).Result;
			// Console.WriteLine(u.Name + u.Surname);
			// Console.WriteLine($"{u.Id} {u.Name} {u.Age}");

			// ICodeRepository codeRepository = new CodeMongoRepository("mongodb://localhost:27017", "Qoollo_name");
			// Console.WriteLine(codeRepository.IsExists("codecode"));
			// codeRepository.Set("codecode", "sun1");
			// codeRepository.Delete("codecode");
			// Console.WriteLine(codeRepository.GetLoginByCode("codecode"));

			Facade facade = new Facade();
			facade.SetUser(new User() { Name = "Name1", Surname = "Surname1", Age = 18, Login = "Danya", Password = "password" });
			// User u = facade.GetUserByLogin("Danya3");
			// Console.WriteLine(u.Name + u.Surname);
			// Console.WriteLine("Ok");
		}

		public static IMongoDatabase MangoMakeConnection() //making connection to DB
		{
			string connectionString = "mongodb://localhost:27017";
			var client = new MongoClient(connectionString);
			var database = client.GetDatabase("Qoolloo_name"); //Conneting to test DB
			return database;

		}
	}
}
