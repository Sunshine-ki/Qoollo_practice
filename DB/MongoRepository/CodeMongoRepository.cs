
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
		public void Set(string code)
		{
			try
			{
				BsonDocument filter = new BsonDocument { { "Code", code } };
				_db.GetCollection<BsonDocument>("UsersCodes").InsertOne(filter);
			}
			catch { Console.WriteLine("Exception mb\n"); }
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
	}
}
