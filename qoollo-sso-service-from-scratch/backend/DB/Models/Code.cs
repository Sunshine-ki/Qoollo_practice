using System;
using MongoDB.Bson;
using MongoDB.Driver;

using MongoDB.Bson.Serialization.Attributes;

namespace QoolloSSO.backend.DataBase.Models
{
	public class UserCode
	{
		// [BsonId]
		public ObjectId _id { get; set; }
		public string Code { get; set; }
		public string Login { get; set; }
	}
}
