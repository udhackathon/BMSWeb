using BMS.Core.Interfaces;
using BMS.Core.SharedKernel;
using BMS.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace bms
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      string dbName = Guid.NewGuid().ToString();
      services.AddDbContext<AppDbContext>(options =>
      options.UseInMemoryDatabase(dbName));
      //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddMvc()
          .AddControllersAsServices();
      //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "API Library - Buffer Management System", Version = "v1" });
      });

      var container = new Container();

      container.Configure(config =>
      {
        config.Scan(_ =>
        {
          _.AssemblyContainingType(typeof(Startup)); // Web
          _.AssemblyContainingType(typeof(BaseEntity)); // Core
          _.Assembly("BMS.Infrastructure"); // Infrastructure
          _.WithDefaultConventions();
          _.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
        });

        // TODO: Add Registry Classes to eliminate reference to Infrastructure

        // TODO: Move to Infrastucture Registry
        config.For(typeof(IRepository<>)).Add(typeof(EfRepository<>));

        //Populate the container using the service collection
        config.Populate(services);
      });

      return container.GetInstance<IServiceProvider>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        //app.UseHsts();
      }

      //app.UseHttpsRedirection();
      //app.UseStaticFiles();
      //app.UseCookiePolicy();

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BMS API - V1");
      });

      app.UseMvc(routes =>
      {
        routes.MapRoute("default", "api/{controller=About}/{action=Get}/{id?}");
      });
    }
  }
}
