using System;

namespace ClientApi
{
	public class QoolloAuth
	{
		public static string GetUriForGetCode(string redirect_uri, string clientName)
		{
			string result = $"http://localhost:5004/Home/Auth/?redirect_uri={redirect_uri}&auth=code&clientName={clientName}";
			return result;
		}
	}
}
