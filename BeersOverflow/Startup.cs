using AspNetCoreDemo.Data;
using AspNetCoreDemo.Helpers;
using AspNetCoreDemo.Repositories;
using AspNetCoreDemo.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using Newtonsoft.Json;

namespace AspNetCoreDemo
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddControllers()
			services.AddControllersWithViews()
				.AddNewtonsoftJson(options =>
				{
					// This prevents the application from crashing when displaying mutually related entities
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});
			
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCoreDemo API", Version = "v1" });
			});

			services.AddDbContext<ApplicationContext>(options =>
			{
				// The connection string has been moved to appsettings.json
				options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
				options.EnableSensitiveDataLogging();
			});

			// Repositories
			services.AddScoped<IBeersRepository, BeersRepository>();
			services.AddScoped<IStylesRepository, StylesRepository>();
			services.AddScoped<IUsersRepository, UsersRepository>();

			// Services
			services.AddScoped<IBeersService, BeersService>();
			services.AddScoped<IStylesService, StylesService>();
			services.AddScoped<IUsersService, UsersService>();

			// Helpers
			services.AddScoped<ModelMapper>();
			services.AddScoped<AuthManager>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseDeveloperExceptionPage();
			app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreDemo API V1");
				options.RoutePrefix = "api/swagger";
			});

			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
