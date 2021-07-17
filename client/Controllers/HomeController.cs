using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;
using System.Web;
using System.Net.Http;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;

namespace client.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			// TODO: 
			// HttpContext.Session.SetString("redirect_uri", redirect_uri);
			_logger = logger;
		}

		public IActionResult SignIn()
		{
			return View();
		}

		public RedirectToActionResult Index()
		{
			// Узнаем, от кого пришел запрос.
			// string str = Request.ServerVariables["REMOTE_ADDR"];
			// Console.WriteLine(str);
			var addr = HttpContext.Connection.LocalIpAddress;
			var port = HttpContext.Connection.LocalPort;
			// уникальный идентификатор для представления этого соединения.
			var id = HttpContext.Connection.Id;
			var ipAddr = HttpContext.Connection.RemoteIpAddress;

			Console.WriteLine($"Address: {addr} port: {port} id: {id} ipAddr: {ipAddr}");

			return RedirectToAction("SignIn", "Home");//, new { a = 10, h = 12 });
		}


		public string Post(string uri, string data, string contentType, string method = "POST")
		{
			byte[] dataBytes = Encoding.UTF8.GetBytes(data);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			request.ContentLength = dataBytes.Length;
			request.ContentType = contentType;
			request.Method = method;

			using (Stream requestBody = request.GetRequestStream())
			{
				requestBody.Write(dataBytes, 0, dataBytes.Length);
			}

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public string GetCode(string code)
		{

			// qoolo_auth.com/get_access_token
			// clientID=j23jhk
			// clientSecret=jsdf23ljkl
			// code=as1kldj8rjdk 
			using (var c = new WebClient())
			{
				var postData = new NameValueCollection();
				// TODO: clientID and clientSecret ???
				postData.Add("clientID", "j23jhk");
				postData.Add("clientSecret", "jsdf23ljkl");
				postData.Add("code", code);
				var response = c.UploadValues("http://localhost:5004/Home/GetAccessToken", "POST", postData);
				string responseInString = Encoding.UTF8.GetString(response);
				Console.WriteLine(responseInString);
			}

			return code;
		}

		public string Auth(string redirect_uri, string code)
		{
			Console.WriteLine($"redirect_uri = {redirect_uri}\ncode = {code}");


			return "Access token = ";
		}

		[HttpPost]
		public void LoginWithQoollo()
		{
			Response.Redirect("http://localhost:5004/Home/Auth/?redirect_uri=localhost:5000/Home/GetCode&auth=code&data=photo&who=mysite");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
