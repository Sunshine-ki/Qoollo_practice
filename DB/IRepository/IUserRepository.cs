using System;
using System.Collections.Generic;

namespace DB
{
	public interface IUserRepository
	{
		List<User> GetUserById(string Id);
		List<User> GetAllUsers();
		void SetUser(User u);
		void UpdateUser(string userOldId, User userNew);
		void DeleteUserById(string Id);
	}
}