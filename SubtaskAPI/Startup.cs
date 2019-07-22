using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SubtaskAPI.Controllers;

namespace SubtaskAPI
{
    public class Startup
    {
        private static readonly WindsorContainer Container = new WindsorContainer();

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Setup component model contributors for making windsor services available to IServiceProvider
            Container.AddFacility<AspNetCoreFacility>(f => f.CrossWiresInto(services));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddLogging((lb) => lb.AddConsole().AddDebug());
            
            // Custom application component registrations, ordering is important here
            RegisterApplicationComponents(services);

            // Castle Windsor integration, controllers, tag helpers and view components, this should always come after RegisterApplicationComponents
            return services.AddWindsor(Container,
                opts => opts.UseEntryAssembly(typeof(ApiController).Assembly), // <- Recommended
                () => services.BuildServiceProvider(validateScopes: false)); // <- Optional
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // For making component registrations of middleware easier
            Container.GetFacility<AspNetCoreFacility>().RegistersMiddlewareInto(app);

            // Add custom middleware, do this if your middleware uses DI from Windsor
            // Container.Register(Component.For<CustomMiddleware>().DependsOn(Dependency.OnValue<ILoggerFactory>(loggerFactory)).LifestyleScoped().AsMiddleware());

            // Add framework configured middleware, use this if you dont have any DI requirements
            //app.UseMiddleware<FrameworkMiddleware>();

            // Serve static files
            //app.UseStaticFiles();

            // Mvc default route
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
            //Container.Register(Component.For<IHttpContextAccessor>().ImplementedBy<HttpContextAccessor>());
            //Container.Register(Component.For<IScopedDisposableService>().ImplementedBy<ScopedDisposableService>().LifestyleScoped().IsDefault());
            //Container.Register(Component.For<ITransientDisposableService>().ImplementedBy<TransientDisposableService>().LifestyleTransient().IsDefault());
            //Container.Register(Component.For<ISingletonDisposableService>().ImplementedBy<SingletonDisposableService>().LifestyleSingleton().IsDefault());
        }
    }
}
