using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using QoolloSSO.backend.DataBase.Models;

namespace QoolloSSO.backend.DataBase.IRepository
{
	public interface IUserRepository
	{
		Task<User> GetByLogin(string login);
		Task<List<User>> GetAll();
		Task Set(User u);
		Task Update(string userOldId, User userNew);
		Task DeleteById(string Id);
	}
}