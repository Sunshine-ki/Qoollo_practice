using System;

using MongoDB.Bson.Serialization.Attributes;

namespace DB
{
	public class User
	{
		[BsonId] public string Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }
	}
}