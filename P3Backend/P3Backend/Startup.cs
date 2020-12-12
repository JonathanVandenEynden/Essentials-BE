using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using P3Backend.Data;
using P3Backend.Data.Repositories;
using P3Backend.Model.RepoInterfaces;
using System.Security.Claims;
using System.Text;

namespace P3Backend {
	public class Startup {

		readonly string EssentialsAllowOrigin = "_essentialsAllowOrigin";

		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration {
			get;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
						);

			services.AddScoped<DataInitializer>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IChangeManagerRepository, ChangeManagerRepository>();
			services.AddScoped<IAdminRepository, AdminRepository>();
			services.AddScoped<IChangeInitiativeRepository, ChangeInitiativeRepository>();
			services.AddScoped<ISurveyRepository, SurveyRepository>();
			services.AddScoped<IChangeGroupRepository, ChangeGroupRepository>();
			services.AddScoped<IOrganizationRepository, OrganizationRepository>();
			services.AddScoped<IProjectRepository, ProjectRepository>();
			services.AddScoped<IRoadmapItemRepository, RoadMapItemRepository>();

			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);



			services.AddCors(options => {
				options.AddPolicy(name: EssentialsAllowOrigin,
								  builder => {
									  //builder.AllowAnyOrigin();

									  builder.WithOrigins("https://essentialstoolkit.netlify.app",
															"https://essentials-angular.azurewebsites.net")
											.AllowAnyHeader()
										   .AllowAnyMethod();

								  });
			});

			services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => {
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters {
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						RequireExpirationTime = true,
						ValidateIssuerSigningKey = true,

						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
					};
				});

			services.AddAuthorization(options => {
				options.AddPolicy("AdminAccess", p => p.RequireClaim(ClaimTypes.Role, "admin"));
				options.AddPolicy("ChangeManagerAccess", p => p.RequireAssertion(c => c.User.HasClaim(ClaimTypes.Role, "admin") || c.User.HasClaim(ClaimTypes.Role, "changeManager")));
				options.AddPolicy("EmployeeAccess", p => p.RequireAssertion(c => c.User.HasClaim(ClaimTypes.Role, "admin") || c.User.HasClaim(ClaimTypes.Role, "changeManager") || c.User.HasClaim(ClaimTypes.Role, "employee")));
			});

			services.Configure<IdentityOptions>(options => {
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
			});

			services.AddSwaggerDocument(c => {
				c.DocumentName = "apidocs";
				c.Title = "EssentialsToolkit";
				c.Version = "v1";
				c.Description = "The essentialsToolkitAPI documentation";
				c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new OpenApiSecurityScheme {
					Type = OpenApiSecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = OpenApiSecurityApiKeyLocation.Header,
					Description = "Copy 'Bearer' + valid JWT token into field"
				}));
				c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
			});

			//services.AddOpenApiDocument(c => { 
			//	c.DocumentName = "apidocs"; 
			//	c.Title = "EssentialsToolkit"; 
			//	c.Version = "v1"; 
			//	c.Description = "The essentialsToolkitAPI documentation"; 
			//});
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

			app.UseCors(EssentialsAllowOrigin);

			app.UseOpenApi();
			app.UseSwaggerUi3();


			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			dataInitializer.InitializeData().Wait();
		}
	}
}
