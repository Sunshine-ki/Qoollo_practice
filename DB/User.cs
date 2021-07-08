using System;

namespace DB
{
	public class User
	{
		public User(int Uid, string name, string surname, int age)
		{
			this.Id = Uid;
			this.UId = Uid;
			this.Name = name;
			this.Surname = surname;
			this.Age = age;
		}
		
		public int Id { get; set; }
		public int UId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }
	}
}