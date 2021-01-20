using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Data;
using Model.Repository;
using Model.UnitOfWork;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcMovie.Helper
{
    public static class InJectionExtension
    {
        /// <summary>
        /// Interface 介面注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Names"></param>
        public static void InJectionByInterface(this IServiceCollection services, List<string> Names)
        {
            Assembly[] assembliesToScan = new[]
            {
                    Assembly.Load("Model"),
                    Assembly.Load("Service")
            };
            foreach (string item in Names)
            {
                services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(c => c.Name.EndsWith(item))
                .AsPublicImplementedInterfaces();
            }
        }

        /// <summary>
        /// Class 類別注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Names"></param>
        public static void InJectionByClass(this IServiceCollection services, List<string> Names)
        {
            Assembly[] assembliesToScan = new[]
             {
                    Assembly.Load("Model"),
                    Assembly.Load("Service")
            };

            foreach (string item in Names)
            {
                services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                    .Where(c => c.Name.EndsWith(item) && !c.IsInterface)
                    .AsPublicImplementedInterfaces();
            };
        }

        /// <summary>
        /// Generic 通用注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblyName"></param>
        /// <param name="Names"></param>
        public static void InJectionByGeneric(this IServiceCollection services, string assemblyName, List<string> Names)
        {
            Assembly assmblys = Assembly.Load(assemblyName);
            foreach (string item in Names)
            {
                List<Type> types = assmblys.GetTypes().Where(x => x.Name.Contains(item) && x.IsGenericType).ToList();
                List<Type> classes = types.Where(x => x.IsClass).ToList();
                List<Type> interfaces = types.Where(x => x.IsInterface).ToList();

                classes.ForEach(x => services.AddScoped(interfaces.Where(s => s.Name.EndsWith(x.Name)).FirstOrDefault(), x));
            }
        }

        /// <summary>
        /// UnitOfWork 注入
        /// </summary>
        /// <param name="services"></param>

        public static void InJectionByUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }

        /// <summary>
        /// GenericRepository 注入
        /// </summary>
        /// <param name="services"></param>

        public static void InJectionByGenericRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        /// <summary>
        /// Service 注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="service"></param>
        public static void InJectionByService(this IServiceCollection services, string service)
        {

            //services.AddScoped<IMoviesService, MoviesService>();
            Assembly assmbly = Assembly.Load(service);
            services.RegisterAssemblyPublicNonGenericClasses(assmbly)
                .Where(c => c.Name.EndsWith(service)).AsPublicImplementedInterfaces();
        }

        /// <summary>
        /// DbContext 注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InJectionByDbContext(this IServiceCollection services, string configuration)
        {
            //services.AddDbContext<DbContext>();
            services.AddDbContext<MvcMovieContext>(options => options.UseSqlServer(configuration));
        }
    }
}
