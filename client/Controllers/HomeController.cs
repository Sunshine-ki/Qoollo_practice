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
using ClientApi;


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

		public IActionResult Privacy()
		{
			return View();
		}


		public RedirectToActionResult Index()
		{
			// Узнаем, от кого пришел запрос.
			// уникальный идентификатор для представления этого соединения.
			// var id = HttpContext.Connection.Id;
			// Console.WriteLine($"id: {id}");

			return RedirectToAction("SignIn", "Home");//, new { a = 10, h = 12 });
		}

		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public void LoginWithQoollo()
		{
			// redirect_uri - по этому адресу вернется code.
			string redirect_uri = "localhost:5000/Home/GetCode";
			string data = "photo and video";
			string clientName = "mysite";

			var uri = ClientApi.QoolloAuth.GetUriForGetCode(redirect_uri, data, clientName);

			Response.Redirect(uri);
			// Response.Redirect("http://localhost:5004/Home/Auth/?redirect_uri=localhost:5000/Home/GetCode&auth=code&data=photo&who=mysite");
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


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}