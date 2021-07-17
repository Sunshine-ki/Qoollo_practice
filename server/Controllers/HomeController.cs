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
using BusinessLogic;

namespace server.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		Facade _facade;
		public HomeController(ILogger<HomeController> logger)
		{
			Facade _facade = new Facade();
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


		public IActionResult Auth(string redirect_uri, string auth, string clientName)
		{
			// TODO: Если сессии нет, то страница авторизации, 
			// если есть, то ридерект на страницу с согласием. 

			var id = HttpContext.Connection.Id;
			Console.WriteLine($"Connection id: {id}");


			HttpContext.Session.SetString("redirect_uri", redirect_uri);
			HttpContext.Session.SetString("client_name", clientName);

			return View();
		}

		public RedirectToActionResult SignIn(server.Models.User user)
		{
			// TODO: С бд сверить есть ли такой пользователь 
			// TODO: password ---> HashPassword
			Console.WriteLine($"SignIn {user.Login} {user.Password}");

			return RedirectToAction("AccessOk", "Home");//, new { a = 10, h = 12 });

			// return RedirectToAction("Accept", "Home");//, new { a = 10, h = 12 });
		}


		public IActionResult Accept()
		{
			// К каким данным и кто хочет получить доступ.
			ViewBag.who = HttpContext.Session.GetString("client_name");
			// ViewBag.data = HttpContext.Session.GetString("data");

			return View();
		}

		public void AccessOk()
		{
			Console.WriteLine("Access!");

			// TODO: Генератор code переписать?
			Facade facade = new Facade();

			string code = facade.CreateCode();

			string redirect_uri = HttpContext.Session.GetString("redirect_uri");

			Response.Redirect("http://" + redirect_uri + "/?code=" + code);
			// Response.Redirect("https://localhost:5001/Home/GetCode/?code=as1kldj8rjdk");
		}

		public string GetAccessToken(string code)
		{
			Facade facade = new Facade();

			if (facade.IsExistsCode(code))
			{
				facade.DeleteCode(code);
				string token = "DATA";
				HttpContext.Session.SetString("token", token);

				// TODO: Тут вернуть token (access_token)
				return token;
			}
			return "null";
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
