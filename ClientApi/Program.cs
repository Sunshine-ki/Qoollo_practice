using System;

namespace ClientApi
{
	class Program
	{
		static void Main(string[] args)
		{
			string redirect_uri = "localhost:5000/Home/Auth";
			// string data = "photo";
			string clientName = "mysite";

			var uri = ClientApi.QoolloAuth.GetUriForGetCode(redirect_uri, clientName, "5004");

			Console.WriteLine(uri);
		}
	}
}
