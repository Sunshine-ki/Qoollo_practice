using System;
// using User;

namespace DB
{
	public class Facade
	{
		public User GetUser()
		{
			return new User(0, "-");
		}

		public void SetUser(User user)
		{

		}


		public void UpdateUser(User userOld, User userNew)
		{

		}

		public void DeleteUserById(int id)
		{

		}
	}
}