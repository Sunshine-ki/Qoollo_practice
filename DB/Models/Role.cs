using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace QoolloSSO.backend.DataBase.Models
{
	public class Role
	{
		[BsonId]
		public string Id { get; set; }
		public string Name { get; set; }
		public List<string> Users { get; set; }
	}
}
