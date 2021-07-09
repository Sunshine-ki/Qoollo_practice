using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using client.Models;
using System.Web;

namespace client.Controllers
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

		public string Auth(string redirect_uri, string code)
		{
			Console.WriteLine($"redirect_uri = {redirect_uri}\ncode = {code}");
			// TODO: Общение с сервером.
			return "Access token = ";
		}

		[HttpPost]
		public void Login_With_Qoolo()
		{
			Response.Redirect("https://localhost:5003/Home/Auth/?redirect_uri=Login_With_Qoolo&auth=code");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
