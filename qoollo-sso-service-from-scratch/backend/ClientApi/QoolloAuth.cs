using System;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace ClientApi
{
	public class QoolloAuth
	{
		public static string GetUriForGetCode(string redirect_uri, string clientName, string ServerPort)
		{
			string result = $"http://localhost:{ServerPort}/Home/Auth/?redirect_uri={redirect_uri}&auth=code&clientName={clientName}";
			return result;
		}

		public static string GetDataByCode(string code, string ServerPort)
		{
			string data;
			using (var c = new WebClient())
			{
				var postData = new NameValueCollection();
				postData.Add("code", code);
				var response = c.UploadValues($"http://localhost:{ServerPort}/Home/GetAccessToken", "POST", postData);
				data = Encoding.UTF8.GetString(response);
				// Console.WriteLine(data);
			}

			return data;
		}
	}
}
