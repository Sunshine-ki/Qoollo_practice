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
		private static Random random = new Random();
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopkrstuvwxyz";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public void AccessOk()
		{
			Console.WriteLine("Access!");
			// TODO: тут из сессии достаем "localhost:5001/Home/GetCode" (это redirect_uti)
			// Прикрепляем код и отправляем.
			// Console.WriteLine(HttpContext.Session.GetString("redirect_uri") + "/?code=" + RandomString(10));

			string code = RandomString(10);
			HttpContext.Session.SetString("code", code);
			Console.WriteLine($"Code in session:{code}");

			HttpContext.Session.SetString("code", code);
			Console.WriteLine("SESSION CODE: " + HttpContext.Session.GetString("code"));
			Response.Redirect("http://" + HttpContext.Session.GetString("redirect_uri") + "/?code=" + code);
			// Response.Redirect("https://localhost:5001/Home/GetCode/?code=as1kldj8rjdk");
		}

		public IActionResult Accept()
		{
			// TODO: Из сессии достать данные о том, какой клиент к каким данным 
			// Хочет получить доступ. Кнопка "Accept".

			ViewBag.who = HttpContext.Session.GetString("who");
			ViewBag.data = HttpContext.Session.GetString("data");

			return View();
		}

		public RedirectToActionResult SignIn(server.Models.User user)
		{
			Console.WriteLine($"SignIn {user.Login} {user.Password}");

			return RedirectToAction("Accept", "Home");//, new { a = 10, h = 12 });
		}

		public IActionResult Auth(string redirect_uri, string auth, string data, string who)
		{
			// TODO: Тут в сессию положить данные 
			// TODO: Если сессии нет, то страница авторизации, 
			// если есть, то ридерект на страницу с согласием. 

			HttpContext.Session.SetString("redirect_uri", redirect_uri);
			HttpContext.Session.SetString("data", data); // Какие данные
			HttpContext.Session.SetString("who", who);

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

		public string TestPostRequest(string postData)
		{
			Console.WriteLine($"postData={postData}");
			return "I am sever!";
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
