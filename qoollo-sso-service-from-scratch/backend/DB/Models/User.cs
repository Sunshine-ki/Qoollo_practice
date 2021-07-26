using System;
using MongoDB.Bson;
using MongoDB.Driver;

using MongoDB.Bson.Serialization.Attributes;

namespace QoolloSSO.backend.DataBase.Models
{
	public class User
	{
		// [BsonId]
		// public string Id { get; set; }
		public ObjectId _id { get; set; }
		public string Login { get; set; }
		public string Name { get; set; }
		public string Name2 { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }
		public int Age { get; set; }
	}
}