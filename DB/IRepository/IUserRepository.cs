using System;
using System.Collections.Generic;

namespace QoolloSSO.backend.DataBase.Models
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