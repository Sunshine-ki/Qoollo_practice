using System;
using System.Collections.Generic;

namespace DB
{
	public interface IUserRepository
	{
		List<User> GetUserById(int uId);
		List<User> GetAllUsers();
		void SetUser(User u);
		void UpdateUser(int userOldId, User userNew);
		void DeleteUserById(int UId);
	}
}