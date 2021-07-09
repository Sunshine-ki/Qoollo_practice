using System;
// TODO: Поправить
// using MongoDB.Bson.Serialization.Attributes;

namespace DB
{
	public class User
	{
		// TODO: Поправить
		// [BsonId]
		public int Id { get; set; }
		public int UId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }
	}
}