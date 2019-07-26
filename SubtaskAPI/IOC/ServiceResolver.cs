using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using NHibernate.Tool.hbm2ddl;
using SubtaskAPI.Logic;
using SubtaskAPI.Repositories;

//using Castle.MicroKernel.Registration;

namespace SubtaskAPI.IOC
{
    public class ServiceResolver
    {
        private static WindsorContainer container;
        private static IServiceProvider serviceProvider;

        public ServiceResolver(IServiceCollection services)
        {
            container = new WindsorContainer();
            

            container.Register(Classes.FromAssembly(Assembly.GetExecutingAssembly())
                .BasedOn<ITaskRepository>().WithService.FromInterface());
            container.Register(Classes.FromAssembly(Assembly.GetExecutingAssembly())
                .BasedOn<ITaskLogic>().WithService.FromInterface());

            // DDL - code first
            //Configuration cfg = new Configuration().Configure();
            //new SchemaExport(cfg).Create(false, true);

            container.Register(Component
                .For<NHibernate.ISessionFactory>()
                .Instance(new Configuration().Configure().BuildSessionFactory()));

            container.Register(Component
                .For<NHibernate.ISession>()
                .UsingFactoryMethod(kernel => kernel.Resolve<NHibernate.ISessionFactory>().OpenSession())
                .LifeStyle.Transient);

            // Automapper
            // Register all mapper profiles
            container.Register(
                Classes.FromAssemblyInThisApplication(GetType().Assembly)
                    .BasedOn<Profile>().WithServiceBase());

            // Register IConfigurationProvider with all registered profiles
            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });
            }).LifestyleSingleton());

            // Register IMapper with registered IConfigurationProvider
            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(kernel =>
                    new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve)));

            serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(container, services);
        }

        public IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
