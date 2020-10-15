using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using P3Backend.Data;
using P3Backend.Data.Repositories;
using P3Backend.Model.RepoInterfaces;

namespace P3Backend {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration {
			get;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<DataInitializer>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IChangeManagerRepository, ChangeManagerRepository>();
			services.AddScoped<IChangeInitiativeRepository, ChangeInitiativeRepository>();
			services.AddScoped<ISurveyRepository, SurveyRepository>();

			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);

			services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<IdentityOptions>(options => {
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
			});

			services.AddSwaggerDocument();

			services.AddOpenApiDocument(c => { c.DocumentName = "apidocs"; c.Title = "RecipeAPI"; c.Version = "v1"; c.Description = "The RecipeAPI documentationdescription."; });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer dataInitializer) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseAuthentication();

			app.UseOpenApi();
			app.UseSwaggerUi3();


			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			dataInitializer.InitializeData().Wait();
		}
	}
}
