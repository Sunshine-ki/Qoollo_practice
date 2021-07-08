using System;

namespace DB
{
	public class User
	{
		public User(int id, string name)
		{
			this.Id = id;
			this.Name = name;
		}

		int Id { get; set; }
		string Name { get; set; }
		string Surname { get; set; }
		int Age { get; set; }
	}
}