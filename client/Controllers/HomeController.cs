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


namespace client.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult SignIn()
		{
			return View();
		}

		public RedirectToActionResult Index()
		{
			return RedirectToAction("SignIn", "Home");//, new { a = 10, h = 12 });
		}

		public IActionResult Privacy()
		{
			// HttpClientHandler clientHandler = new HttpClientHandler();
			// clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

			// // Pass the handler to httpclient(from you are calling api)
			// HttpClient client = new HttpClient(clientHandler);

			// using (var wb = new WebClient())
			// {
			// 	var data = new NameValueCollection();
			// 	data["username"] = "myUser";
			// 	data["password"] = "myPassword";
			// 	data["postData"] = "postData!";

			// 	var response = wb.UploadValues("https://localhost:5003/Home/TestPostRequest", "POST", data);
			// 	string responseInString = Encoding.UTF8.GetString(response);
			// }

			return View();
		}

		public string GetCode(string code)
		{
			return code;
		}

		public string Auth(string redirect_uri, string code)
		{
			Console.WriteLine($"redirect_uri = {redirect_uri}\ncode = {code}");
			// TODO: Общение с сервером.
			return "Access token = ";
		}

		[HttpPost]
		public void LoginWithQoolo()
		{
			Response.Redirect("https://localhost:5003/Home/Auth/?redirect_uri=localhost:5001/Home/GetCode&auth=code&data=photo&who=mysite");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
