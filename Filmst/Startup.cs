using System.Text;
using AutoMapper;
using AutoMapper.Configuration;
using DAL;
using DAL.Entities;
using Filmst.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.AspNetCore.Swagger;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Filmst
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			_cfg = new MapperConfigurationExpression();
			_container = new Container();
		}

		public IConfiguration Configuration { get; }

		private readonly MapperConfigurationExpression _cfg;
		private readonly Container _container;

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			IntegrateSimpleInjector(services);

			Bootstrap();

			services.AddDbContext<ApplicationContext>(options => 
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<User, IdentityRole<long>>(opts =>
				{
					opts.Password.RequireNonAlphanumeric = false;
				})
				.AddEntityFrameworkStores<ApplicationContext>();

			services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = Configuration["AuthOptions:ISSUER"],
						ValidateAudience = true,
						ValidAudience = Configuration["AuthOptions:AUDIENCE"],
						ValidateLifetime = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AuthOptions:KEY"])),
						ValidateIssuerSigningKey = true,
					};
				});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			InitializeContainer(app);

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseMvc();
		}

		private void IntegrateSimpleInjector(IServiceCollection services)
		{
			_container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
			_container.Options.AllowOverridingRegistrations = true;

			services.AddSingleton<IControllerActivator>(
				new SimpleInjectorControllerActivator(_container));
			services.AddSingleton<IViewComponentActivator>(
				new SimpleInjectorViewComponentActivator(_container));

			services.AddHttpContextAccessor();

			services.EnableSimpleInjectorCrossWiring(_container);
			services.UseSimpleInjectorAspNetRequestScoping(_container);
		}

		private void InitializeContainer(IApplicationBuilder app)
		{
			// Add application presentation components:
			_container.RegisterMvcControllers(app);
			_container.RegisterMvcViewComponents(app);

			// Allow Simple Injector to resolve services from ASP.NET Core.
			_container.AutoCrossWireAspNetComponents(app);
		}

		private void Bootstrap()
		{
			Bootstrapper.Bootstrap(_container);
			MapperBootstrapper.Bootstrap(_cfg);

			PLL.IoC.Bootstrapper.Bootstrap(_container);
			PLL.IoC.MapperBootstrapper.Bootstrap(_cfg);

			Mapper.Initialize(_cfg);
		}
	}
}
