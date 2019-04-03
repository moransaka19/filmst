using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Filmst.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

		private MapperConfigurationExpression _cfg;
		private Container _container;

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			AddDbContext(services);

			IntegrateSimpleInjector(services);

			Bootstrap();

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

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseHttpsRedirection();
			app.UseMvc();
		}

		private void AddDbContext(IServiceCollection services)
		{
			var AddDbContext = typeof(EntityFrameworkServiceCollectionExtensions)
				.GetMethod("AddDbContext", 1,
					new Type[]
					{
						typeof(IServiceCollection),
						typeof(Action<DbContextOptionsBuilder>),
						typeof(ServiceLifetime),
						typeof(ServiceLifetime)
					});

			var applicationContextType = Assembly
				.Load("DAL")
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(DbContext)))
				.Single();

			AddDbContext
				.MakeGenericMethod(applicationContextType)
				.Invoke(services, new object[]
				{
					services,
					(Action<DbContextOptionsBuilder>)(options =>
						options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))),
					ServiceLifetime.Scoped,
					ServiceLifetime.Scoped
				});
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
