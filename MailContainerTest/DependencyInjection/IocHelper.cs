using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MailContainerTest.DependencyInjection
{
    public static class IocHelper
    {
        private static IServiceCollection _serviceCollection;

        public static IServiceCollection ServiceCollection => _serviceCollection ?? (_serviceCollection = new ServiceCollection());

        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider => _serviceProvider ?? (_serviceProvider = ServiceCollection.BuildServiceProvider());

        public static void RegisterSingleton<TType, TImp>() where TImp : TType
        {
            ServiceCollection.AddSingleton(typeof(TType), typeof(TImp));
        }

        public static void RegisterTransient<TType, TImp>() where TImp : TType
        {
            ServiceCollection.AddTransient(typeof(TType), typeof(TImp));
        }

        public static void RegisterScoped<TType, TImp>() where TImp : TType
        {
            ServiceCollection.AddScoped(typeof(TType), typeof(TImp));
        }

        public static void RegisterSingleton<TService>(TService serviceInstance) where TService : class
        {
            ServiceCollection.AddSingleton(serviceInstance);
        }

        public static TService Resolve<TService>()
        {
            return ServiceProvider.GetService<TService>();
        }

        public static IEnumerable<TService> GetAllInstance<TService>()
        {
            return ServiceProvider.GetServices<TService>();
        }

        public static void Build()
        {
            ServiceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Registers singleton types without interface.
        /// </summary>
        /// <param name="asm">The assembly.</param>
        /// <param name="typeEndsWith">The type ends with.</param>
        /// <param name="baseType">Type of the base class.</param>
        /// <param name="lifetime">The service lifetime.</param>
        public static void RegisterAssemblyTypes(Assembly asm, string typeEndsWith, Type baseType, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var types = from tp in asm.GetTypes()
                        where tp.IsClass && tp.Name.EndsWith(typeEndsWith) && tp.IsSubclassOf(baseType)
                        select tp;

            foreach (var itemType in types)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        ServiceCollection.AddTransient(Type.GetType(itemType.FullName));
                        break;
                    case ServiceLifetime.Scoped:
                        ServiceCollection.AddScoped(Type.GetType(itemType.FullName));
                        break;
                    default:
                        ServiceCollection.AddSingleton(Type.GetType(itemType.FullName));
                        break;
                }
            }
        }

        /// <summary>
        /// Registers types with their interface.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <param name="typeEndsWith">The type ends with.</param>
        /// <param name="lifetime">The service lifetime.</param>
        public static void RegisterAssemblyTypes(Assembly asm, string typeEndsWith, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            var types = from tm in asm.GetTypes()
                        where tm.IsClass && tm.Name.EndsWith(typeEndsWith)
                        select tm;

            foreach (var itemType in types)
            {
                var itemInterface = itemType.FindInterfaces(InterfaceFilter, itemType.Name).FirstOrDefault();
                //Ensures the class has implemented an interface with same name with "I" prefixed.
                if (itemInterface == null) continue;
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        ServiceCollection.AddTransient(Type.GetType(itemInterface.FullName), Type.GetType(itemType.FullName));
                        break;
                    case ServiceLifetime.Scoped:
                        ServiceCollection.AddScoped(Type.GetType(itemInterface.FullName), Type.GetType(itemType.FullName));
                        break;
                    default:
                        ServiceCollection.AddSingleton(Type.GetType(itemInterface.FullName), Type.GetType(itemType.FullName));
                        break;
                }
            }
        }

        /// <summary>
        /// Makes sure the class implements an interface with same name with "I" prefixed.
        /// Good coding practice!
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="filterCriteria">The filter criteria.</param>
        /// <returns></returns>
        private static bool InterfaceFilter(Type type, object filterCriteria)
        {
            return type.ToString().EndsWith("I" + filterCriteria, StringComparison.OrdinalIgnoreCase);
        }
    }
}

