using System;
using System.Collections.Generic;
using QoolloSSO.backend.DataBase.Models;

namespace QoolloSSO.backend.DataBase.IRepository
{
	public interface ICodeRepository
	{
		void Set(string code, string Login);
		bool IsExists(string code);
		void Delete(string userCode);
		string GetLoginByCode(string userCode);
	}
}