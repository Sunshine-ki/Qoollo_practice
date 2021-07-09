using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;

namespace server.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public void Access()
		{
			Console.WriteLine("Access!");
			Response.Redirect("https://localhost:5001/Home/Auth/?redirect_uri=qoolo_auth&code=as1kldj8rjdk");
		}

		public IActionResult Check(server.Models.User user)
		{
			Console.WriteLine($"Check {user.Login} {user.Password}");
			return View();
		}

		public IActionResult Auth(string redirect_uri, string auth)
		{
			Console.WriteLine($"redirect_uri = {redirect_uri}\nauth = {auth}");
			return View();
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
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
