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
			if (HttpContext.Request.Cookies.ContainsKey("id_5020"))
			{
				Console.WriteLine($"Куки есть!");
				return RedirectToAction("Welcome", "Home");//, new { a = 10, h = 12 });
			}
			// string name = HttpContext.Request.Cookies["name"];

			return RedirectToAction("LoginWithQoollo", "Home");//, new { a = 10, h = 12 });
		}

		public IActionResult Welcome()
		{
			// HttpContext.Response.Cookies.Append("id", id);
			string id = HttpContext.Request.Cookies["id_5020"];
			string data = "";
			foreach (var kvp in GlobalData.tokens) // kvp = key-value-par
				if (kvp.Key == id)
				{
					data = kvp.Value;
					break;
				}
			ViewBag.data = data;
			return View();
		}

		public IActionResult SignIn()
		{
			return View();
		}

		// [HttpPost]
		public void LoginWithQoollo()
		{
			// redirect_uri - по этому адресу вернется code.
			string redirect_uri = "localhost:5020/Home/AuthOk";
			// TODO: Убрать clientName и data
			// string data = "photo and video";
			string clientName = "mysite";

			var uri = ClientApi.QoolloAuth.GetUriForGetCode(redirect_uri, clientName);

			Response.Redirect(uri);
			// Response.Redirect("http://localhost:5004/Home/Auth/?redirect_uri=localhost:5020/Home/GetCode&auth=code&data=photo&who=mysite");
		}

		public RedirectToActionResult AuthOk(string code)
		{
			string data;
			using (var c = new WebClient())
			{
				var postData = new NameValueCollection();
				postData.Add("code", code);
				var response = c.UploadValues("http://localhost:5004/Home/GetAccessToken", "POST", postData);
				data = Encoding.UTF8.GetString(response);
				Console.WriteLine(data);
			}

			Random random = new Random();
			string id = Convert.ToString(random.Next());

			Console.WriteLine("Куки установлены!");
			HttpContext.Response.Cookies.Append("id_5020", id);
			GlobalData.tokens.Add(new KeyValuePair<string, string>(id, data));

			// ViewBag.data = data;
			// return View();
			return RedirectToAction("Index", "Home");//, new { a = 10, h = 12 });
		}



		public string Auth(string redirect_uri, string code)
		{
			Console.WriteLine($"redirect_uri = {redirect_uri}\ncode = {code}");


			return "Access token = ";
		}


		// public string TokenExists(string token)
		// {

		// 	return "TokenExists";
		// }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}