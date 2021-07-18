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
using Program;

namespace client.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public RedirectToActionResult Index()
		{
			if (HttpContext.Request.Cookies.ContainsKey("id_" + Constants.ClientPort))
				return RedirectToAction("Welcome", "Home");
			return RedirectToAction("LoginWithQoollo", "Home");
		}

		public IActionResult Welcome()
		{
			string id = HttpContext.Request.Cookies["id_" + Constants.ClientPort];
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

		public void LoginWithQoollo()
		{
			// redirect_uri - по этому адресу вернется code.
			string redirect_uri = $"localhost:{Constants.ClientPort}/Home/AuthOk";
			string clientName = "mysite";
			var uri = ClientApi.QoolloAuth.GetUriForGetCode(redirect_uri, clientName, Constants.ServerPort);
			Response.Redirect(uri);
		}


		public RedirectToActionResult AuthOk(string code)
		{
			string data = ClientApi.QoolloAuth.GetDataByCode(code, Constants.ServerPort);

			Random random = new Random();
			string id = Convert.ToString(random.Next());
			HttpContext.Response.Cookies.Append("id_" + Constants.ClientPort, id);
			GlobalData.tokens.Add(new KeyValuePair<string, string>(id, data));
			Console.WriteLine("Cookies installed");

			return RedirectToAction("Index", "Home");
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}