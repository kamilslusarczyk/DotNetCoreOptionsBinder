using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionsBinder.Configuration
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds singleton service for each class which inherits from IOptionsModel and fill this model with content of configuration matched by name convention.
        /// </summary>
        /// <exception cref="MissingMethodException">Will be thrown if class that inherits from IOptionsModel won't have parameterless constructor'</exception>
        public static IServiceCollection AddModelBasedOptions(this IServiceCollection service, IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            assemblies
                .SelectMany(x => x.GetReferencedAssemblies())
                .Distinct()
                .Where(y => assemblies.Any(a => a.FullName == y.FullName) == false)
                .ToList()
                .ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(x)));

            var type = typeof(IOptionsModel);
            var interfaceImplementations = assemblies.SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass);

            foreach (var interfaceImplementation in interfaceImplementations)
            {
                var instance = CreateImplementationInstance(interfaceImplementation);
                configuration.Bind(interfaceImplementation.Name, instance);

                service.AddSingleton(interfaceImplementation, instance);
            }

            return service;
        }

        private static object CreateImplementationInstance(Type interfaceImplementation)
        {
            try
            {
                return Activator.CreateInstance(interfaceImplementation);
            }
            catch (Exception)
            {
                throw new MissingMethodException("Every class that inherits from IOptionsModel needs to have parameterless constructor");
            }
        }
    }
}
