
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using QoolloSSO.backend.DataBase.Models;
using QoolloSSO.backend.DataBase.IRepository;

namespace QoolloSSO.backend.DataBase.MongoRepository
{
	public class CodeMongoRepository : ICodeRepository
	{
		IMongoDatabase _db;
		public CodeMongoRepository(IMongoDatabase db) { _db = db; }
		public CodeMongoRepository(string connectionString, string databaseString)
		{
			var client = new MongoClient(connectionString);
			_db = client.GetDatabase(databaseString);
		}
		public void Set(string code, string login)
		{
			try
			{
				// BsonDocument filter = new BsonDocument { { "Code", code } };
				_db.GetCollection<UserCode>("UsersCodes").InsertOne(new UserCode { Code = code, Login = login });
			}
			catch { Console.WriteLine("Exception mb\n"); }
			Console.WriteLine("Insert OK");
		}
		public bool IsExists(string code)
		{
			var filter = new BsonDocument("Code", code);
			var codeInDB = _db.GetCollection<BsonDocument>("UsersCodes").Find(filter).ToList();
			return System.Convert.ToBoolean(codeInDB.Count);
		}
		public void Delete(string code)
		{
			if (!IsExists(code))
				return;

			BsonDocument filter = new BsonDocument { { "Code", code } };
			_db.GetCollection<BsonDocument>("UsersCodes").DeleteOne(filter);
		}

		public string GetLoginByCode(string userCode)
		{
			if (!IsExists(userCode))
				return null;

			var filter = new BsonDocument("Code", userCode);
			var codeInDB = _db.GetCollection<UserCode>("UsersCodes").Find(filter).ToList();
			return codeInDB[0].Login;
		}

	}
}
