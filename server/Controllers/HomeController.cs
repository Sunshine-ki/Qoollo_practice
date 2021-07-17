using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Specialized;

namespace server.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Auth(string redirect_uri, string auth, string data, string clientName)
		{
			// TODO: Если сессии нет, то страница авторизации, 
			// если есть, то ридерект на страницу с согласием. 

			HttpContext.Session.SetString("redirect_uri", redirect_uri);
			HttpContext.Session.SetString("data", data); // Какие данные
			HttpContext.Session.SetString("client_name", clientName);

			return View();
		}

		public RedirectToActionResult SignIn(server.Models.User user)
		{
			// TODO: С бд сверить есть ли такой пользователь 
			// TODO: password ---> HashPassword
			Console.WriteLine($"SignIn {user.Login} {user.Password}");

			return RedirectToAction("Accept", "Home");//, new { a = 10, h = 12 });
		}


		public IActionResult Accept()
		{
			// К каким данным и кто хочет получить доступ.
			ViewBag.who = HttpContext.Session.GetString("client_name");
			ViewBag.data = HttpContext.Session.GetString("data");

			return View();
		}

		public void AccessOk()
		{
			Console.WriteLine("Access!");

			string code = server.Generator.RandomString(10);
			string redirect_uti = HttpContext.Session.GetString("redirect_uri");

			// HttpContext.Session.SetString("code", code);
			// Console.WriteLine("SESSION CODE: " + HttpContext.Session.GetString("code"));

			Response.Redirect("http://" + redirect_uti + "/?code=" + code);
			// Response.Redirect("https://localhost:5001/Home/GetCode/?code=as1kldj8rjdk");
		}

		public string GetAccessToken(string code)
		{
			// Console.WriteLine(postData);
			string realCode = HttpContext.Session.GetString("data");
			// string userCode = postData["code"];

			// string who = HttpContext.Session.GetString("who");
			// Console.WriteLine(who);
			// string values = string.Join(",", postData.AllKeys.SelectMany(key => postData.GetValues(key)));
			Console.WriteLine($"data: {realCode} userCode: {code}");

			// TODO: Тут вернуть access_token
			return "!!!I am sever!!!";
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
