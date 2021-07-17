using System;
using QoolloSSO.backend.DataBase;


using QoolloSSO.backend.DataBase.IRepository;
using QoolloSSO.backend.DataBase.MongoRepository;

namespace BusinessLogic
{
	public class Facade
	{
		string _connection = "mongodb://localhost:27017";
		string _nameCollections = "Qoolloo_name";
		ICodeRepository _codeRepository;
		public Facade() { _codeRepository = new CodeMongoRepository(_connection, _nameCollections); }

		public string CreateCode()
		{
			string code = server.Generator.RandomString(10);

			if (_codeRepository.IsExists(code))
				return null;

			_codeRepository.Set(code);
			return code;
		}

		public void DeleteCode(string code)
		{
			ICodeRepository _codeRepository = new CodeMongoRepository(_connection, _nameCollections);
			_codeRepository.Delete(code);
		}

		public bool IsExistsCode(string code)
		{
			ICodeRepository _codeRepository = new CodeMongoRepository(_connection, _nameCollections);
			return _codeRepository.IsExists(code);
		}



	}
}
