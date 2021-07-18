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

		public IActionResult Auth(string redirect_uri, string auth, string clientName)
		{
			HttpContext.Session.SetString("redirect_uri", redirect_uri);
			HttpContext.Session.SetString("client_name", clientName);

			if (HttpContext.Session.Keys.Contains("code"))
				return RedirectToAction("CodeExists", "Home");
			return View();
		}

		public RedirectToActionResult SignIn(server.Models.User user)
		{
			// TODO: С бд сверить есть ли такой пользователь 
			// TODO: password ---> HashPassword
			// Console.WriteLine($"SignIn {user.Login} {user.Password}");
			return RedirectToAction("AccessOk", "Home");
		}

		public void CodeExists()
		{
			string code = HttpContext.Session.GetString("code");
			string redirect_uri = HttpContext.Session.GetString("redirect_uri");
			Response.Redirect("http://" + redirect_uri + "/?code=" + code);
		}
		public void AccessOk()
		{
			Console.WriteLine("Access!");
			Facade facade = new Facade();
			string code = facade.CreateCode();
			string redirect_uri = HttpContext.Session.GetString("redirect_uri");
			HttpContext.Session.SetString("code", code);
			Response.Redirect("http://" + redirect_uri + "/?code=" + code);
		}

		public string GetAccessToken(string code)
		{
			if (code == null)
				return "";
			Facade facade = new Facade();

			if (facade.IsExistsCode(code))
			{
				// facade.DeleteCode(code);
				string token = "DATA";
				return token;
			}
			return "";
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
