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

			if (HttpContext.Session.Keys.Contains("visited"))
				return RedirectToAction("AccessOk", "Home");
			return View();
		}

		public RedirectToActionResult SignIn(server.Models.User user)
		{
			Console.WriteLine($" user.Login {user.Login}");

			// Console.WriteLine($"SignIn {user.Login} {user.Password}");
			Facade facade = new Facade();
			string password = facade.GetPasswordByLogin(user.Login);

			Console.WriteLine(password);
			if (user.Password != password)
			{
				return RedirectToAction("BadPassword", "Home");
			}

			HttpContext.Session.SetString("userLogin", user.Login);
			HttpContext.Session.SetString("visited", "true");

			return RedirectToAction("AccessOk", "Home");
		}

		public void AccessOk()
		{
			Console.WriteLine("Access!");
			string login = HttpContext.Session.GetString("userLogin");
			Facade facade = new Facade();
			string code = facade.CreateCode(login);
			string redirect_uri = HttpContext.Session.GetString("redirect_uri");
			Response.Redirect("http://" + redirect_uri + "/?code=" + code);
		}

		public string GetAccessToken(string code)
		{
			if (code == null)
				return "";
			Facade facade = new Facade();

			Console.WriteLine($"code {code}\nIsExistsCode:{facade.IsExistsCode(code)}\nLogin: {facade.GetLoginByCode(code)} ");
			if (facade.IsExistsCode(code))
			{
				string token = facade.GetLoginByCode(code);
				// TODO: Тут по login'у можно получить уже все данные и вернуть их.
				facade.DeleteCode(code);
				return token;
			}
			return "";
		}

		public IActionResult BadPassword()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
