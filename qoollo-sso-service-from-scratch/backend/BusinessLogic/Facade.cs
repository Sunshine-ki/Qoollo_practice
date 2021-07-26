using System;
using System.Threading.Tasks;

using QoolloSSO.backend.DataBase.IRepository;
using QoolloSSO.backend.DataBase.MongoRepository;
using QoolloSSO.backend.DataBase.Models;

namespace BusinessLogic
{
	public class Facade
	{
		string _connection = "mongodb://localhost:27017";
		string _dbName = "Qoollo_name";
		ICodeRepository _codeRepository;
		IUserRepository _repositoryUser;
		public Facade()
		{
			_codeRepository = new CodeMongoRepository(_connection, _dbName);
			_repositoryUser = new UserMongoRepository(_connection, _dbName);

		}

		public string GetLoginByCode(string code)
		{
			return _codeRepository.GetLoginByCode(code);
		}

		public string CreateCode(string login)
		{
			string code = server.Generator.RandomString(10);

			if (_codeRepository.IsExists(code))
				return null;

			_codeRepository.Set(code, login);
			return code;
		}

		public void DeleteCode(string code)
		{
			ICodeRepository _codeRepository = new CodeMongoRepository(_connection, _dbName);
			_codeRepository.Delete(code);
		}

		public bool IsExistsCode(string code)
		{
			ICodeRepository _codeRepository = new CodeMongoRepository(_connection, _dbName);
			return _codeRepository.IsExists(code);
		}

		public string GetPasswordByLogin(string login)
		{
			User u = Task.Run(() => _repositoryUser.GetByLogin(login)).Result;
			// Console.WriteLine(u.Name + u.Surname);
			return u.Password;
		}
		public void SetUser(User u)
		{
			_repositoryUser.Set(u);
		}
	}
}
