using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace client
{
	public static class GlobalData
	{
		public static List<KeyValuePair<string, string>> tokens { get; set; }
	}

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			GlobalData.tokens = new List<KeyValuePair<string, string>>();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// For session.
			// services.AddDistributedMemoryCache();
			// services.AddSession();

			//    services.AddDataProtection()
			// .PersistKeysToAzureBlobStorage(new Uri("<blobUriWithSasToken>"))
			// .ProtectKeysWithAzureKeyVault(new Uri("<keyIdentifier>"), new DefaultAzureCredential());

			services.AddControllersWithViews();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			// app.UseSession();   // добавляем механизм работы с сессиями

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
