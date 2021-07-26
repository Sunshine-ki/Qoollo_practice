using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

using QoolloSSO.backend.DataBase.IRepository;
using QoolloSSO.backend.DataBase.MongoRepository;

namespace QoolloSSO.backend.DataBase.Models
{
	public class Facade
	{
		IUserRepository _repositoryUser;
		string connection = "mongodb://localhost:27017";
		string dbName = "Qoollo_name";
		public Facade()
		{
			_repositoryUser = new UserMongoRepository(connection, dbName);
		}

		public User GetUserByLogin(string login)
		{
			User u = Task.Run(() => _repositoryUser.GetByLogin(login)).Result;
			// Console.WriteLine(u.Name + u.Surname);
			return u;
		}
		public void SetUser(User u)
		{
			_repositoryUser.Set(u);
			// Console.WriteLine($"{u.Id} {u.Name} {u.Age}");
		}
	}
}